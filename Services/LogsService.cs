using LicencaApi.Data;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LicencaApi.Services
{
    public class LogsService : ILogsService
    {
       private readonly LicencaDbContext _context;
       private readonly ILogger<LogsService> _logger;
       private readonly ILogsRepository _logsRepository;

        public LogsService(ILogger<LogsService> logger, LicencaDbContext context, ILogsRepository logsRepository)
        {
            _context = context;
            _logger = logger;
            _logsRepository = logsRepository;
        }
        public async Task<IEnumerable<LicencaLogModel>> GetLogs()
       {
            _logger.LogInformation("Buscando logs de licença no banco de dados.");
            var logs = await _logsRepository.GetAllLogsAsync();
            return logs;
        }
    }
}
