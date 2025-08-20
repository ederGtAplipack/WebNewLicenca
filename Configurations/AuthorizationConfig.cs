using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace LicencaApi.Configurations
{
    public static class AuthorizationConfig
    {
        public static IServiceCollection AddAppAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                // Política para usuários com o papel 'Admin'
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));

                // Política para usuários com o papel 'User'
                options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));

                // Política para usuários com o papel 'SuperAdmin' e a claim 'id' com valor 'root'
                options.AddPolicy("SuperAdminOnly", policy =>
                    policy.RequireRole("SuperAdmin")
                          .RequireClaim("id", "root"));

                // Política de acesso exclusivo: usuário tem a claim 'key' com valor 'root' OU está no papel 'SUPERADMIN'
                options.AddPolicy("ExclusiveAccess", policy =>
                    policy.RequireAssertion(context =>
                        context.User.HasClaim(claim => claim.Type == "key" && claim.Value == "root") ||
                        context.User.IsInRole("SUPERADMIN")));
            });

            return services;
        }
    }
}