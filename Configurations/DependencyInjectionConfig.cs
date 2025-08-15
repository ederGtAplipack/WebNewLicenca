using LicencaApi.Auth;
using LicencaApi.Data;
using LicencaApi.Helpers;
using LicencaApi.Interfaces;
using LicencaApi.Repositories;
using LicencaApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LicencaApi.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {

            services.AddScoped<ILicencaService, LicencaService>();
            services.AddScoped<ILicencaRepository, LicencaRepository>();    
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(LicencaMapper));
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            //services.AddAuthentication("Bearer").AddJwtBearer();                     
            services.AddAuthorization();
            services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Configuração do Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<LicencaDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
