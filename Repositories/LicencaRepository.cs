using LicencaApi.Data;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using LicencaApi.Services;
using Microsoft.EntityFrameworkCore;

/*A classe LicencaRepository é responsável por realizar operações CRUD (Create, Read, Update, Delete) na tabela Licenca. Ela implementa os métodos definidos na interface ILicencaRepository, garantindo que outras partes do sistema possam interagir com os dados sem precisar conhecer os detalhes da implementação.

Os métodos são assíncronos (async) para melhorar a performance em cenários onde operações de I/O são realizadas, como consultas ao banco de dados. Isso evita bloqueios no thread principal e melhora a escalabilidade da aplicação.*/
namespace LicencaApi.Repositories
{
    public class LicencaRepository : ILicencaRepository
    {
        private readonly LicencaDbContext _context;
        private readonly ILogger<LicencaService> _logger;

        public LicencaRepository(LicencaDbContext context, ILogger<LicencaService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public LicencaRepository(LicencaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LicencaModel>> BuscarTodasAsync()
        {
            return await _context.Licenca.ToListAsync();
            _logger.LogInformation("Buscando todas as licenças.");
        }

        public async Task<LicencaModel?> BuscarPorIdAsync(int id)
        {
            return await _context.Licenca.FindAsync(id);
        }

        public async Task CriarAsync(LicencaModel model)
        {
            await _context.Licenca.AddAsync(model);
        }

        public Task AtualizarAsync(LicencaModel model)
        {
            _context.Licenca.Update(model);
            return Task.CompletedTask;
        }

        public async Task SalvarAsync()
        {
           //wait _context.SaveChangesAsync();
        }
    }
}
