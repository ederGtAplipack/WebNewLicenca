using LicencaApi.Data;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LicencaApi.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly LicencaDbContext _context;
        private readonly ILogger<ClienteRepository> _logger;

        public ClienteRepository(LicencaDbContext context, ILogger<ClienteRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AnagraficaModel?> BuscarPorIdCliente(int id)
        {
            _logger.LogInformation("Passando pelo Repository do BuscarPorIdCliente {id}.", id);
            return await _context.Anagrafica.FindAsync(id);
        }

        public async Task<IEnumerable<AnagraficaModel>> BuscarTodasAnagrafica()
        {
            _logger.LogInformation("Passando pelo ClienteRepository.BuscarTodasAnagrafica.");
            return await _context.Anagrafica.ToListAsync();
        }

        public async Task CreateNewAnagrafica(AnagraficaModel anagraficaModel)
        {
            _logger.LogInformation("Passando pelo Repository do CreateNewAnagrafica.");
            await _context.Anagrafica.AddAsync(anagraficaModel);
        }

        public void AtualizarAnagrafica(AnagraficaModel model)
        {
            _logger.LogInformation("Passando pelo ClienteRepository.AtualizarAnagrafica.");
            _context.Entry(model).State = EntityState.Modified;
        }

        public async Task DeleteAnagraficaAsync(AnagraficaModel anagrafica)
        {

            _logger.LogInformation("Passando pelo Repository do DeleteAnagrafica.");
            _context.Anagrafica.Remove(anagrafica);

        }
    }
}