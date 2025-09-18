using LicencaApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LicencaApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ContratoController : ControllerBase
    {
        private readonly ILogger<ContratoController> _logger;
        private readonly IContratoService _contratoService;

        public ContratoController(ILogger<ContratoController> logger, IContratoService contratoService)
        {
            _logger = logger;
            _contratoService = contratoService;
        }
        [HttpGet("AllContrato")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation("Iniciando Busca por Contratos");
                            //VAI PARA O public interface IContratoService
                var contrato = await _contratoService.BuscarTodosContratos();

                _logger.LogInformation("Fim da Busca por contratos");
                return Ok(contrato);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erros ao buscar Contratos");
                return StatusCode(500, "Erro interno do Servidor");

            }
        }
    }
}
