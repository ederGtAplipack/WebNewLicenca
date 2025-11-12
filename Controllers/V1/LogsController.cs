using LicencaApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LicencaApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LogsController : ControllerBase
    {
        public readonly ILogsService _logsService;
        public readonly ILogger<LogsController> _logger;
        public LogsController(ILogger<LogsController> logger, ILogsService logsService)
        {
            _logger = logger;
            _logsService = logsService;
        }

        [HttpGet("LicencaLog")]
        public async Task<IActionResult> GetLogs()
        {
            try
            {
                var logs = await _logsService.GetLogs();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro GetLogs");
                return StatusCode(500, "Erro interno");
            }
        }
    }
}
