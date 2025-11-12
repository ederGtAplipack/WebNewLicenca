using LicencaApi.Data;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LicencaApi.Repositories
{
    public class LogsRepository : ILogsRepository
    {
        private readonly LicencaDbContext _context;
        private readonly ILogger<LogsRepository> _logger;
        public LogsRepository(LicencaDbContext context, ILogger<LogsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<LicencaLogModel>> GetAllLogsAsync()
        {
            _logger.LogInformation("Recuperando todos os logs de licença do banco de dados.");
            return await _context.LicencaLog.ToListAsync();
        }
    }
}
