using LicencaApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LicencaApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RevendaController : ControllerBase
    {
        private readonly ILogger<RevendaController> _logger;
        private readonly IRevendaService _revendaService;

        public RevendaController(ILogger<RevendaController> logger, IRevendaService revendaService)
        {
            _logger = logger;
            _revendaService = revendaService;
        }

        [HttpGet("AllRevenda")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation("Iniciando busca por todas Revenda");
                var revendas = await _revendaService.BuscarTodasRevendas();

                _logger.LogInformation("Fim da Busca por todas Revendas");
                return Ok(revendas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar Revendas");
                return StatusCode(500, "Erro interno do servidor");
            }
        }       
      
    }
}
