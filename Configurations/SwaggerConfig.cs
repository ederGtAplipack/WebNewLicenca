using Asp.Versioning.ApiExplorer;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.OpenApi.Models;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();


            foreach (var description in provider.ApiVersionDescriptions)
            {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {     
            
                        Title = "Licenca API",
                        Version = "v1",
                        Description = "API for managing licenses",
                        Contact = new OpenApiContact
                    {
                        Name = "Support Team",
                        Email = "contato@aplipack.com.br"
                    }
                });
            }    
                // 🔐 Autenticação Bearer no Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira o token JWT dessa forma: Bearer {seu token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        });

        return services;
    }
}
