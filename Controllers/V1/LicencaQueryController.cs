using LicencaApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LicencaApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LicencaQueryController : ControllerBase
    {
        private readonly ILicencaService _service;
        private readonly ILogger<LicencaQueryController> _logger;

        public LicencaQueryController(ILicencaService service, ILogger<LicencaQueryController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /* Método para obter todas as licenças com detalhes adicionais.
         * Retorna uma lista de LicencaDetalhadaDTO que inclui informações detalhadas sobre cada licença.
         */
        [HttpGet("GetAllWithDetails")]
        public async Task<IActionResult> GetAllWithDetails()
        {
            var licencas = await _service.ObterTodasComDetalhesAsync();
            return Ok(licencas);
        }
    }
}
