using Humanizer;
using LicencaApi.DTOs;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using LicencaApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdRevenda(int id)
        {
            try
            {
                _logger.LogInformation("RevendaController GetById method called with id: {Id}", id);
                var revenda = await _revendaService.BuscarPorIdRevenda(id);

                if (revenda == null)
                    return NotFound();
                _logger.LogWarning("Revenda com ID {Id} encontrada", id);

                return Ok(revenda);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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
        [Authorize]
        [HttpPost("CreateNewRevenda")]
        public async Task<IActionResult> CreateRevenda(CriarRevendaDTO dto)
        {
            _logger.LogInformation("Criando nova Revenda");
            // Lógica para obter o ID do usuário logado
            var idUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(idUser))
            {
                return Unauthorized("Usuário não autenticado ou ID de usuário não encontrado no token.");
            }

            try
            {
                var novaRevenda = await _revendaService.CriarNewRevenda(dto, idUser);
                return CreatedAtAction(nameof(GetAll), new { id = novaRevenda.idRevenda }, novaRevenda);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar nova Revenda");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpPut("UpdateRevenda/{id:int}")]
        public async Task<IActionResult> UpdateRevenda(int id, AtualizarRevendaDTO dto)
        {
            _logger.LogInformation("Iniciando atualização de Revenda com ID {Id}", id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var atualizado = await _revendaService.AtualizarAsync(id, dto);
            if (!atualizado)
                return NotFound();

            _logger.LogInformation("Revenda com ID {Id} atualizada com sucesso", id);
            return NoContent();
        }

        [HttpDelete("DeleteRevenda/{id:int}")]
        public async Task<IActionResult> DeleteRevenda(int id)
        {
            var revenda = await _revendaService.BuscarPorIdRevenda(id);
            if (revenda == null)
                return NotFound("Revenda não Encontrado");
            _logger.LogInformation("Revenda não encontrado");

            var deletado = await _revendaService.DeletarRevenda(id);
            if (!deletado)
                return NotFound();
            _logger.LogInformation("Revenda com ID {id} removido !");
            return Ok(new { mensagem = "Revenda Removido", id });
        }

        /*Para retornar todos os clientes associados a uma revenda específica.*/
        [HttpGet("{idRevenda}/ClientForRevenda")]
        public async Task<IActionResult> ListarClientesPorRevenda(int idRevenda)
        {
            _logger.LogInformation("RevendaController: buscando clientes para a revenda com ID {idRevenda}.", idRevenda);
            try
            {
                var clientes = await _revendaService.ListarClientesPorRevendaAsync(idRevenda);

                if (clientes == null || !clientes.Any())
                {
                    _logger.LogWarning("Nenhum cliente encontrado para a revenda com ID {idRevenda}.", idRevenda);
                    return NotFound("Nenhum cliente encontrado para esta revenda.");
                }

                _logger.LogInformation("Clientes encontrados para a revenda {idRevenda}.", idRevenda);
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar clientes por revenda.");
                return StatusCode(500, "Erro interno do servidor ao buscar clientes.");
            }
        }
    }
}
