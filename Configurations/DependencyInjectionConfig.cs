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
            services.AddScoped<IClienteService, ClienteService>(); 

            services.AddScoped<IContratoService, ContratoService>();
            services.AddScoped<IContratoRepository, ContratoRepository>();
            
            services.AddScoped<ILicencaRepository, LicencaRepository>();    
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(LicencaMapper));
                        
            services.AddAppAuthorization();
            services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
            
            // Configuração do Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<LicencaDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
