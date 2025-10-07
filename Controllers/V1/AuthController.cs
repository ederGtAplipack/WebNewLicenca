using LicencaApi.Auth;
using LicencaApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LicencaApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ITokenService tokenService, IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AuthController> logger)
        {
            _tokenService = tokenService;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [Authorize]
        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var roleManager = HttpContext.RequestServices.GetService(typeof(RoleManager<IdentityRole>)) as RoleManager<IdentityRole>;
            if (roleManager == null)
            {
                _logger.LogError("RoleManager service is not available.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "RoleManager service is not available." });
            }
            // Verifica se o nome do papel é nulo ou vazio
            if (await roleManager.RoleExistsAsync(roleName))
            {
                _logger.LogWarning("Role {RoleName} already exists.", roleName);
                return BadRequest(new { Message = "Role already exists." });
            }
            var result = await roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
            {
                _logger.LogInformation("Role {RoleName} created successfully.", roleName);
                return Ok(new { Message = "Role created successfully." });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error creating role.", result.Errors });
            }
        }

        //[Authorize]
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole(AssignRoleModel assignRoleModel, string roleName)
        {
            var user = await _userManager.FindByNameAsync(assignRoleModel.Username);
            if (user == null)
            {
                _logger.LogWarning("User {Username} not found.", assignRoleModel.Username);
                return NotFound(new { Message = "User not found." });
            }
            var roleManager = HttpContext.RequestServices.GetService(typeof(RoleManager<IdentityRole>)) as RoleManager<IdentityRole>;
            if (roleManager == null)
            {
                _logger.LogError("RoleManager service is not available.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "RoleManager service is not available." });
            }
            // Verifica se o papel existe
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                _logger.LogWarning("Role {RoleName} does not exist.", roleName);
                return BadRequest(new { Message = "Role does not exist." });
            }
            // Verifica se o usuário já tem o papel
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                _logger.LogInformation("User {Username} assigned to role {RoleName} successfully.", assignRoleModel.Username, roleName);
                return Ok(new { Message = "User assigned to role successfully." });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error assigning role.", result.Errors });
            }
        }
        [Authorize]
        [HttpPost("RemoveRole")]
        public async Task<IActionResult> RemoveRole(RegisterModel removeRoleModel, string roleName)
        {
            var user = await _userManager.FindByNameAsync(removeRoleModel.Username);
            if (user == null)
            {
                _logger.LogWarning("User {Username} not found.", removeRoleModel.Username);
                return NotFound(new { Message = "User not found." });
            }
            var roleManager = HttpContext.RequestServices.GetService(typeof(RoleManager<IdentityRole>)) as RoleManager<IdentityRole>;
            if (roleManager == null)
            {
                _logger.LogError("RoleManager service is not available.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "RoleManager service is not available." });
            }
            // Verifica se o papel existe
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                _logger.LogWarning("Role {RoleName} does not exist.", roleName);
                return BadRequest(new { Message = "Role does not exist." });
            }
            // Verifica se o usuário tem o papel
            if (!await _userManager.IsInRoleAsync(user, roleName))
            {
                _logger.LogWarning("User {Username} is not in role {RoleName}.", removeRoleModel.Username, roleName);
                return BadRequest(new { Message = "User is not in the specified role." });
            }
            // Remove o papel do usuário
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                _logger.LogInformation("User {Username} removed from role {RoleName} successfully.", removeRoleModel.Username, roleName);
                return Ok(new { Message = "User removed from role successfully." });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error removing role.", result.Errors });
            }
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
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim("key", user.key!),
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
                    refreshToken,
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
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        [Route("Revoke/{username}")]
        public async Task<IActionResult> Revoke()
        {
            if (User.Identity?.IsAuthenticated != true)
            {
                return Unauthorized(new { Status = "Error", Message = "Unauthorized" });
            }
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

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = _userManager.Users.ToList(); // pega todos do AspNetUsers

                var userList = new List<object>();

                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    userList.Add(new
                    {
                        user.Id,
                        user.UserName,
                        user.Email,
                        user.key,      // se essa propriedade existir no seu IdentityUser extendido
                        Roles = roles
                    });
                }

                return Ok(userList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var roleManager = HttpContext.RequestServices.GetService(typeof(RoleManager<IdentityRole>)) as RoleManager<IdentityRole>;
                if (roleManager == null)
                {
                    _logger.LogError("RoleManager service is not available.");
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "RoleManager service is not available." });
                }
                var roles = roleManager.Roles.ToList();
                var roleList = roles.Select(role => new
                {
                    role.Id,
                    role.Name
                });
                return Ok(roleList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }

        }
    }
}
