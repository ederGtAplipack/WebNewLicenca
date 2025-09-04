using LicencaApi.DTOs;
using LicencaApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LicencaApi.Controllers.V1
{
    public class AtivacaoController : ControllerBase
    {
        private readonly ILicencaService _service;
        private readonly ILogger<AtivacaoController> _logger;
        public AtivacaoController(ILicencaService service, ILogger<AtivacaoController> logger)
        {
            _service = service;
            _logger = logger;
        }
        /*
        * O método ProcessarAtivacaoDispositivo é responsável por processar a ativação de um dispositivo.
        * Ele recebe um objeto AtivacaoDispositivoRequestDTO contendo os dados necessários para a ativação.
        * Se a ativação for bem-sucedida, retorna um objeto AtivacaoDispositivoResponseDTO com os detalhes da licença ativada.
        * Em caso de erro, retorna um status 500 (Internal Server Error).
        */
        [HttpPost("ativar-processar")]
        public async Task<IActionResult> ProcessarAtivacaoDispositivo([FromBody] AtivacaoDispositivoRequestDTO request)
        {
            try
            {
                var response = await _service.ProcessarAtivacaoDispositivoAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar ativação de dispositivo");
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
}
