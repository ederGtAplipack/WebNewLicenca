using Humanizer;
using LicencaApi.DTOs;
using LicencaApi.Interfaces;
using LicencaApi.Models;
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
    }
}
