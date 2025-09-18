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

        public async Task<IEnumerable<RevendaModel>> BuscarTodasAnagrafica()
        {
            _logger.LogInformation("Passando pelo RevendasRepository.BuscarTodasRevendas.");
            return await _context.Revenda.ToListAsync();
        }
    }
}
