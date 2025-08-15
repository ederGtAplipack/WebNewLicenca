using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LicencaApi.Auth
{
    /*Este método recupera o principal de reivindicações (ClaimsPrincipal) a partir de um token expirado. 
     * Ele aceita como parâmetros um token (que está expirado) e uma configuração (configuration). 
     * O ClaimsPrincipal permite acessar as informações contidas nas reivindicações do token, mesmo que ele não esteja mais válido.*/
    public interface ITokenService
    {
        JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration _configuration);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _configuration);

    }
}
