using LicencaApi.Data;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LicencaApi.Repositories
{
    public class SoftwareRepository : ISoftwareRepository
    {
        private readonly ILogger<SoftwareRepository> _logger;
        private readonly LicencaDbContext _context;
        public SoftwareRepository(ILogger<SoftwareRepository> logger, LicencaDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IEnumerable<SoftwareModel>> GetAllSoftwaresAsync()
        {
            _logger.LogInformation("Passando pelo Repository do GetAllSoftwaresAsync.");
            return await _context.Softwares.ToListAsync();
        }
    }
}
