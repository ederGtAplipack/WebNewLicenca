using LicencaApi.Interfaces;
using LicencaApi.Models;
using LicencaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LicencaApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SoftwareController : ControllerBase
    {
        private readonly ILogger<SoftwareController> _logger;
        private readonly ISoftwareService _softwareService;

        public SoftwareController(ILogger<SoftwareController> logger, ISoftwareService softwareService)
        {
            _logger = logger;
            _softwareService = softwareService;
        }

        [HttpGet("AllSoftware")]
        public async Task<IActionResult> GetAllSoftware()
        {
            try
            {
                _logger.LogInformation("Iniciando busca por todos os Softwares");
                // Aqui você chamaria o serviço para obter os dados reais
                var softwares = await _softwareService.GetAllSoftwaresAsync();
                _logger.LogInformation("Fim da Busca por todos os Softwares");
                return Ok(softwares);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar Softwares");
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
}
