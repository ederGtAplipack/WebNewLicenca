using LicencaApi.Data;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LicencaApi.Repositories
{
    public class RevendaRepository : IRevendaRepository
    {
        private readonly LicencaDbContext _context;
        private readonly ILogger<RevendaRepository> _logger;

        public RevendaRepository (LicencaDbContext context, ILogger<RevendaRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<RevendaModel?> BuscarPorIdRevenda(int id)
        {
            _logger.LogInformation("Passando pelo Repository do BuscarPorIdRevenda {id}.", id);
            return await _context.Revenda.FindAsync(id);
        }

        public async Task AssociateRevendaUser(RevendaUserModel revendaUser)
        {
            _logger.LogInformation("Passando pelo RevendaRepository.AssociateRevendaUser.");
            await _context.RevendaUser.AddAsync(revendaUser); 
        }
         

        public async Task<IEnumerable<RevendaModel>> BuscarTodasRevendas()
        {
            _logger.LogInformation("Passando pelo RevendasRepository.BuscarTodasRevendas.");
            return await _context.Revenda.ToListAsync();
        }

        public async Task CreateNewRevenda(RevendaModel revenda)
        {
            _logger.LogInformation("Passando pelo RevendaRepository.CreateNovaRevenda.");
            await _context.Revenda.AddAsync(revenda);
        }

        public async void DeleteRevendaAsync(RevendaModel revenda)
        {
            _logger.LogInformation("Passando pelo Repository do DeleteRevenda.");
            _context.Revenda.Remove(revenda);
        }

        public async Task<IEnumerable<AnagraficaModel>> ListarClientesPorRevendaAsync(int idRevenda)
        {
            _logger.LogInformation("Buscando clientes para a revenda com ID {idRevenda}.", idRevenda);
            return await _context.Anagrafica
                .Where(c => c.IdRevenda == idRevenda)
                .ToListAsync();

        }
    }
}
