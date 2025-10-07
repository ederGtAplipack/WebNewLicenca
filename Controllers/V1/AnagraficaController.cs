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
        private readonly IClienteService _clienteService;
        public AnagraficaController(ILogger<AnagraficaController> logger, IClienteService clienteService)
        {
            _logger = logger;
            _clienteService = clienteService;
        }

        [HttpGet("AllAnagrafica")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation("Iniciando busca por todos Clientes");
                var anagrafica = await _clienteService.BuscarTodosClientes();                

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
                var anagrafica = await _clienteService.BuscarPorIdCliente(id);

                if (anagrafica == null)
                    return NotFound();
                _logger.LogWarning("Cliente com ID {Id} encontrada", id);

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
                var novaAnagrafica = await _clienteService.CriarNovoCliente(dtoAnagrafica);
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
        [HttpPut("UpdateAnagrafica/{id:int}")]
        public async Task<IActionResult> UpdateAnagrafica(int id, AtualizarAnagraficaDTO dto)
        {
            _logger.LogInformation("Iniciando atualização de Cliente com ID {Id}", id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            /*if (id != dto.NumLic)
                return BadRequest("ID de licença inválido.");*/

            var atualizado = await _clienteService.AtualizarAsync(id, dto);
            if (!atualizado)
                return NotFound();

            _logger.LogInformation("Cliente com ID {Id} atualizada com sucesso", id);
            return NoContent();
        }

        [HttpDelete("DeleteAnagrafica/{id:int}")]
        public async Task<IActionResult> DeleteAnagrafica(int id)
        {
            var anagrafica = await _clienteService.BuscarPorIdCliente(id);
            if (anagrafica == null)
                return NotFound("Cliente não Encontrado");
            _logger.LogInformation("Cliente não encontrado");

            var deletado = await _clienteService.DeletarCliente(id);
            if (!deletado)
                return NotFound();
            _logger.LogInformation("Cliente com ID {id} removido !");
            return Ok(new { mensagem = "Cliente Removido", id });
        }
    }
}
