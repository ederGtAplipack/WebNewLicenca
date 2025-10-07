using Microsoft.AspNetCore.Cors.Infrastructure;

namespace LicencaApi.Configurations
{
    public static class CorsConfig
    {
        public static IServiceCollection AddAppCors(this IServiceCollection services, IConfiguration configuration)
        {
            // Pega a URL do frontend do arquivo de configuração (appsettings.json)
            var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    // Em produção, use WithOrigins para maior segurança
                    if (allowedOrigins.Length > 0)
                    {
                        policy.WithOrigins(allowedOrigins)
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    }
                    else
                    {
                        // Em desenvolvimento, ou se não houver origens configuradas, use AllowAnyOrigin
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    }
                });
            });

            return services;
        }
    }
}