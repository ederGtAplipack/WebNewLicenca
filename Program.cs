using LicencaApi.Controllers;
using LicencaApi.Data;
using LicencaApi.Mappings;
using LicencaApi.Models;
using LicencaApi.Repositories;
using LicencaApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Configuração do banco de dados
        var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
        builder.Services.AddDbContext<LicencaDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        //builder.Services.AddScoped<LicencaController>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<ILicencaService, LicencaService>();
        builder.Services.AddScoped<ILicencaRepository, LicencaRepository>();

        builder.Services.AddAutoMapper(typeof(LicencaProfile));
        
        builder.WebHost.ConfigureKestrel(serverOptions =>
        {
            // Configurações adicionais do Kestrel, se necessário
            serverOptions.Limits.MaxRequestBodySize = 10 * 1024 * 1024; // Exemplo: 10 MB
            serverOptions.ListenAnyIP(8080);
        });


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();

        app.UseAuthorization();

        app.MapControllers();



        // Endpoint GET para recuperar todas as licenças
        /*app.MapGet("/licenca", async (LicencaDbContext db) =>
        {
            var licencas = await db.Licencas.ToListAsync();
            return Results.Ok(licencas);
        })
        .WithName("ObterTodasLicencas")
        .WithOpenApi();
                       

        // Endpoint GET para recuperar licença por ID
        app.MapGet("/licenca/{id}", async (int id, LicencaDbContext db) =>
        {
            var licenca = await db.Licencas.FindAsync(id);
            if (licenca == null)
            {
                return Results.NotFound($"Licença com ID {id} não encontrada.");
            }
            return Results.Ok(licenca);
        })
        .WithName("ObterLicencaPorId")
        .WithOpenApi();

        // Endpoint POST para registrar licença
        app.MapPost("/licenca/registrar", async (LicencaModel licenca, LicencaDbContext db) =>
        {
            licenca.NumLic = new Random().Next(1, 10000);
            licenca.DataLic = DateTime.Now;
            licenca.Attivo = true;

            db.Licencas.Add(licenca);
            await db.SaveChangesAsync();

            return Results.Created($"/licenca/{licenca.NumLic}", licenca);
        })
        .WithName("RegistrarLicenca")
        .WithOpenApi();

        app.MapPut("/licenca/editar/{id}", async (int id, LicencaModel licenca, LicencaDbContext db) =>
        {
            var existingLicenca = await db.Licencas.FirstOrDefaultAsync(x => x.NumLic == id);
            if (existingLicenca == null)
            {
                return Results.NotFound($"Licença com ID {id} não encontrada.");
            }
            //existingLicenca.IdCliente = licenca.IdCliente;
            existingLicenca.TipoLic = licenca.TipoLic;
            existingLicenca.MacAddress = licenca.MacAddress;
            existingLicenca.DataLic = licenca.DataLic;
            existingLicenca.Scade = licenca.Scade;
            existingLicenca.Software = licenca.Software;
            existingLicenca.Ip = licenca.Ip;
            existingLicenca.Attivo = licenca.Attivo;
            existingLicenca.IdRevenda = licenca.IdRevenda;
            existingLicenca.SistemaOp = licenca.SistemaOp;
            existingLicenca.DataAtivacao = licenca.DataAtivacao;
            existingLicenca.Tipo_Pc = licenca.Tipo_Pc;
            existingLicenca.Nome_Computador = licenca.Nome_Computador;
            existingLicenca.Processador = licenca.Processador;
            existingLicenca.IdLicencaChave = licenca.IdLicencaChave;
            await db.SaveChangesAsync();
            return Results.Ok(existingLicenca);
        })
        .WithName("EditarLicenca")
        .WithOpenApi();
        */
     
        app.Run();
    }
}




