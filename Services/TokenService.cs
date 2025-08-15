using LicencaApi.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LicencaApi.Services
{
    public class TokenService : ITokenService
    {
        public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration _configuration)
        {
            // Retrieve the secret key from configuration
            var key = _configuration.GetSection("JwtSettings").GetValue<string>("Key") ??
                      throw new InvalidOperationException("Key is not configured in JwtConfiguration");

            // Convert the key to a byte array
            var privateKey = Encoding.UTF8.GetBytes(key);

            // Create signing credentials using the private key and HMAC SHA256 algorithm
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(privateKey),
                SecurityAlgorithms.HmacSha256Signature);

            // Create the token descriptor with the claims, signing credentials, and expiration time
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_configuration.GetSection("JwtSettings")
                                            .GetValue<double>("ExpirationAccessTokenTimeMinutes")),

                Audience = _configuration.GetSection("JwtSettings").GetValue<string>("ValidAudience") ??
                          throw new InvalidOperationException("ValidAudience is not configured in JwtConfiguration"),
                Issuer = _configuration.GetSection("JwtSettings").GetValue<string>("ValidIssuer") ??
                          throw new InvalidOperationException("ValidIssuer is not configured in JwtConfiguration"),

                SigningCredentials = signingCredentials
            };
            // Create the JWT token handler and generate the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return token;
        }

        public string GenerateRefreshToken()
        {
            // Generate a random string for the refresh token
            var randomNumber = new byte[128];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            // Fill the byte array with random bytes
            randomNumberGenerator.GetBytes(randomNumber);
            // Convert the byte array to a Base64 string
            var tokenRefresh = Convert.ToBase64String(randomNumber);

            return tokenRefresh;
        }

        //
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _configuration)
        {
            // Retrieve the secret key from configuration
            var key = _configuration.GetSection("JwtSettings").GetValue<string>("Key") ??
                      throw new InvalidOperationException("Key is not configured in JwtConfiguration");
            // Convert the key to a byte array
            var privateKey = Encoding.UTF8.GetBytes(key);
            // Create token validation parameters
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(privateKey),
                ValidIssuer = _configuration.GetSection("JwtSettings").GetValue<string>("ValidIssuer") ??
                              throw new InvalidOperationException("ValidIssuer is not configured in JwtConfiguration"),
                ValidAudience = _configuration.GetSection("JwtSettings").GetValue<string>("ValidAudience") ??
                               throw new InvalidOperationException("ValidAudience is not configured in JwtConfiguration"),

                ValidateLifetime = false // We want to allow expired tokens
            };
            // Create a token handler and validate the token
            var tokenHandler = new JwtSecurityTokenHandler();

            // Validate the token and retrieve the ClaimsPrincipal
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            // Check if the token is a valid JWT and uses HMAC SHA256 algorithm
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}