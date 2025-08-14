using LicencaApi.Configurations;
using LicencaApi.Data;
using LicencaApi.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

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

// 
builder.Services.AddAppServices();

/*builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ILicencaRepository, LicencaRepository>();
builder.Services.AddScoped<ILicencaService, LicencaService>();*/

// Configuração do JWT

//builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthentication("Bearer").AddJwtBearer();
builder.Services.AddAuthorization();


// Controller + Swagger
builder.Services.AddControllers().AddJsonOptions(options=>options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddSwaggerConfiguration();

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.MapScalarApiReference();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
