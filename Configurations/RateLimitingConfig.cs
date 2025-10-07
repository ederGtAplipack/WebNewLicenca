using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace LicencaApi.Configurations
{
    public static class RateLimitingConfig
    {
        public static IServiceCollection AddAppRateLimiting(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                // Define a política 'fixed' para ser usada em endpoints específicos
                options.AddFixedWindowLimiter(policyName: "fixed", fixedOptions =>
                {
                    fixedOptions.PermitLimit = 5;
                    fixedOptions.Window = TimeSpan.FromSeconds(10);
                });

                // A política padrão (para todos os endpoints não anotados)
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });

            return services;
        }
    }
}