using LicencaApi.DTOs;
using LicencaApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LicencaApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AnagraficaController : ControllerBase
    {
        private readonly ILogger<AnagraficaController> _logger;
        private readonly ILicencaService _service;
        public AnagraficaController(ILogger<AnagraficaController> logger, ILicencaService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("AllAnagrafica")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation("Iniciando busca por todos Clientes");
                var anagrafica = await _service.BuscarTodasAnagrafica();

                _logger.LogInformation("Fim da Busca por todos Clientes");
                return Ok(anagrafica);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar Clientes");
                return StatusCode(500, "Erro interno do servidor");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAnagrafica(int id)
        {
            try
            {
                _logger.LogInformation("AnagraficaController GetById method called with id: {Id}", id);
                var anagrafica = await _service.BuscarPorIdAnagrafica(id);

                if (anagrafica == null)
                    return NotFound();
                _logger.LogWarning("Licença com ID {Id} encontrada", id);

                return Ok(anagrafica);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }           

        }

        [HttpPost("CreateAnagrafica")]
        public async Task<IActionResult> CreateAnagrafica(CriarAnagraficaDTO dtoAnagrafica)
        {
            try
            {
                _logger.LogInformation("AnagraficaController CreateAnagrafica method called");
                var novaAnagrafica = await _service.CriarNovaAnagrafica(dtoAnagrafica);
                if (novaAnagrafica == null)
                {
                    _logger.LogWarning("Falha ao criar nova Anagrafica");
                    return StatusCode(500, "Erro ao criar Anagrafica");
                }
                _logger.LogInformation("Nova Anagrafica criada com sucesso, ID: {Id}", novaAnagrafica.IdAnagrafica);
                return CreatedAtAction(nameof(GetByIdAnagrafica), new { id = novaAnagrafica.IdAnagrafica }, novaAnagrafica);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
