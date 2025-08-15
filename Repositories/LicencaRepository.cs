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
        private readonly ILogger<LicencaRepository> _logger;

        public LicencaRepository(LicencaDbContext context, ILogger<LicencaRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<LicencaModel>> BuscarTodasAsync()
        {
            _logger.LogInformation("Passando pelo LicencaRepository.");
            return await _context.Licenca.ToListAsync();
        }

        public async Task<LicencaModel?> BuscarPorIdAsync(int id)
        {
            _logger.LogInformation("Passando pelo Repository do BuscarPorIdAsync {id}.", id);
            return await _context.Licenca.FindAsync(id);
        }

        public async Task CriarAsync(LicencaModel model)
        {
            _logger.LogInformation("Passando pelo Repository do Create.");
            await _context.Licenca.AddAsync(model);
        }

        public void AtualizarAsync(LicencaModel model)
        {
            _logger.LogInformation("Passando pelo Repository do Update.");
            _context.Entry(model).State = EntityState.Modified;
        }

        private async Task<bool> LicencaExists(int id)
        {
            _logger.LogInformation("Verificando se a licença existe no Repository.");
            return await _context.Licenca.AnyAsync(e => e.NumLic == id);           
        }

        public async Task SalvarAsync()
        {
           //wait _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LicencaModel>> BuscarAtivasAsync()
        {
            // Aqui você pode implementar a lógica para buscar licenças ativas
            _logger.LogInformation("Passando pelo Repository das ativas.");
            return await _context.Licenca.Where(l => l.Attivo).ToListAsync();
        }

        public async Task<bool> DesativarAsync(int id)
        {
            var licenca = await BuscarPorIdAsync(id);
            if (licenca == null) return false;
            licenca.Attivo = false;
            return true;
        }

    }
}
