using Asp.Versioning.ApiExplorer;
using LicencaApi.Configurations;
using LicencaApi.Data;
using LicencaApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// DBContext
var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
builder.Services.AddDbContext<LicencaDbContext>(options =>
           options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

/*if (!builder.Environment.IsDevelopment())
{
    // Configuração do Kestrel
    builder.WebHost.UseKestrel(serverOptions =>
    {
        serverOptions.Limits.MaxRequestBodySize = 10 * 1024 * 1024; // 10 MB
        serverOptions.ListenAnyIP(8080);
    });
}*/

// AutoMapper
builder.Services.AddAutoMapper(typeof(LicencaMapper));

// Configuração do CORS
builder.Services.AddAppCors(builder.Configuration);

// Configuração do Rate Limiting
builder.Services.AddAppRateLimiting();

// Configuração de Dependências (atenção: remova o AddControllers() de lá!)
builder.Services.AddAppServices();

// Configuração do JWT
builder.Services.AddJwtAuthentication(builder.Configuration);

//Habilita o uso do IAuthorizationMiddlewareResultHandler personalizado
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();

// Controllers + Swagger
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"C:\inetpub\API\DataProtection-Keys"))
    .SetApplicationName("LicencaApi");

// Configuração de Versionamento de API
builder.Services.AddVersioningConfiguration();
builder.Services.AddSwaggerConfiguration();

builder.WebHost.UseIIS();
builder.WebHost.UseIISIntegration();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                 $"Licenca API { description.GroupName.ToUpperInvariant()}"
            );
        }
        options.RoutePrefix = "swagger";// Swagger na raiz
    });
}

app.UseCors();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => Results.Ok("API Licenca rodando no IIS!"));
app.Run();
