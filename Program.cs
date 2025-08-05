using LicencaApi.Data;
using LicencaApi.Helpers;
using LicencaApi.Interfaces;
using LicencaApi.Repositories;
using LicencaApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DBContext
var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
builder.Services.AddDbContext<LicencaDbContext>(options =>
           options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configuração do Kestrel
builder.WebHost.UseKestrel(serverOptions =>
{
    // Configurações adicionais do Kestrel, se necessário
    serverOptions.Limits.MaxRequestBodySize = 10 * 1024 * 1024; // Exemplo: 10 MB
    serverOptions.ListenAnyIP(8080);
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(LicencaMapper));

// DI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ILicencaRepository, LicencaRepository>();
builder.Services.AddScoped<ILicencaService, LicencaService>();

// Controller + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
