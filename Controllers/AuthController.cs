using LicencaApi.Auth;
using LicencaApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LicencaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(ITokenService tokenService, IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _tokenService = tokenService;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel loginRequest)
        {
            var user = await _userManager.FindByNameAsync(loginRequest.Username!);

            //se o usuario for encontrado e senha for igual a amazenada 
            if (user is not null && await _userManager.CheckPasswordAsync(user, loginRequest.Password!))
            {
                //OBTER AS ROLES
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                //incluindo as clains/roles na lista desse usuario
                foreach (var claim in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, claim));
                }

                var token = _tokenService.GenerateAccessToken(authClaims, _configuration);

                var refreshToken = _tokenService.GenerateRefreshToken();
                // Salvar o refresh token no usuário
                _ = int.TryParse(_configuration["JwtSettings:ExpirationRefreshTokenTimeMinutes"], out int expirationRefreshTokenTimeMinutes);

                if (expirationRefreshTokenTimeMinutes <= 0)
                {
                    expirationRefreshTokenTimeMinutes = 60; // Default to 60 minutes if not set or invalid
                }
                user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(expirationRefreshTokenTimeMinutes);

                user.RefreshToken = refreshToken;

                await _userManager.UpdateAsync(user);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    refreshToken = refreshToken,
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();

        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterModel registerRequest)
        {
            var user = new ApplicationUser
            {
                UserName = registerRequest.Username,
                Email = registerRequest.Email,
                key = registerRequest.Key,
                ValidIssuer = _configuration["JwtSettings:ValidIssuer"],
                ValidAudience = _configuration["JwtSettings:ValidAudience"]
            };
            var result = await _userManager.CreateAsync(user, registerRequest.Password!);
            if (result.Succeeded)
            {
                return Ok(new { message = "User registered successfully" });
            }
            return BadRequest(new { errors = result.Errors.Select(e => e.Description) });
        }

        [HttpPost]
        [Route("Refresh-Token")]
        public async Task<IActionResult> RefreshToken(TokenModel refreshTokenModel)
        {
            if (refreshTokenModel == null)
            {
                return BadRequest(new { Status = "Error", Message = "Invalid client request" });
            }
            var principal = _tokenService.GetPrincipalFromExpiredToken(refreshTokenModel.AccessToken!, _configuration);
            var username = principal.Identity?.Name;

            if (username == null)
            {
                return BadRequest(new { Status = "Error", Message = "Invalid client request" });
            }

            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.RefreshToken != refreshTokenModel.RefreshToken)
            {
                return BadRequest(new { Status = "Error", Message = "Invalid client request" });
            }

            // Generate new token
            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList(), _configuration);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            // Update user's refresh token and expiry time
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(int.Parse(_configuration["JwtSettings:ExpirationRefreshTokenTimeMinutes"]));

            await _userManager.UpdateAsync(user);

            return Ok(new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            });
        }
        [Authorize]
        [HttpPost]
        [Route("Revoke/{username}")]
        public async Task<IActionResult> Revoke()
        {
            var username = User.Identity?.Name;
            var user = await _userManager.FindByNameAsync(username!);
            if (user == null)
            {
                return BadRequest(new { Status = "Error", Message = "Invalid client request" });
            }
            // Remove the refresh token
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = DateTime.MinValue;
            await _userManager.UpdateAsync(user);
            return NoContent();
        }
    }
}
