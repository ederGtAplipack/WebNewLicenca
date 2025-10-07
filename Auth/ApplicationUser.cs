using Microsoft.AspNetCore.Identity;

namespace LicencaApi.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public string? key { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }

    }
}
