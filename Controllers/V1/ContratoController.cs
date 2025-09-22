using LicencaApi.DTOs;
using LicencaApi.Interfaces;
using LicencaApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdContrato(int id)
        {
            try
            {
                _logger.LogInformation("ContratoController GetById method called with id: {Id}", id);
                var contrato = await _contratoService.BuscarPorIdContrato(id);

                if (contrato == null)
                    return NotFound();
                _logger.LogWarning("Contrato com ID {Id} encontrada", id);

                return Ok(contrato);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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

        [HttpPost("CreateNewContrato")]
        public async Task<IActionResult> CreateContrato(CriarContratoDTO dto)
        {
            try
            {
               _logger.LogInformation("ContratoController CreateContrato method called");
               var contrato = await _contratoService.CriarNovoContrato(dto);
                if (contrato == null)
                {
                      _logger.LogWarning("Falha ao criar nova Contrato");
                      return StatusCode(500, "Erro ao criar Contrato");
                }
                _logger.LogInformation("Novo Contrato criada com sucesso, ID: {Id}", contrato);
                return CreatedAtAction(nameof(GetByIdContrato), new { id = contrato.IdContrato }, contrato);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar novo Contrato");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpPut("UpdateContrato/{id:int}")]
        public async Task<IActionResult> UpdateContrato(int id, AtualizarContratoDTO dto)
        {
            _logger.LogInformation("Iniciando atualização de Contrato com ID {Id}", id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var atualizado = await _contratoService.AtualizarAsync(id, dto);
            if (!atualizado)
                return NotFound();

            _logger.LogInformation("Contrato com ID {Id} atualizada com sucesso", id);
            return NoContent();
        }


        [HttpDelete("DeleteContrato/{id:int}")]
        public async Task<IActionResult> DeleteContrato(int id)
        {
            var contrato = await _contratoService.BuscarPorIdContrato(id);
            if (contrato == null)
                return NotFound("Contrato não Encontrado");
            _logger.LogInformation("Contrato não encontrado");

            var deletado = await _contratoService.DeletarContrato(id);
            if (!deletado)
                return NotFound();
            _logger.LogInformation("Contrato com ID {id} removido !");
            return Ok(new { mensagem = "Contrato Removido", id });
        }
    }
}
