using LicencaApi.DTOs;
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            try
            {
                _logger.LogInformation("SoftwareController GetById method called with id: {Id}", id);
                var software = await _softwareService.GetByIdAsync(id);
                if (software == null)
                    return NotFound();
                _logger.LogWarning("Software com ID {Id} encontrada", id);
                return Ok(software);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

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

        [HttpPost("CreateNewSoftware")]
        public async Task<IActionResult> CreateSoftware(CriarSoftwareDTO dto)
        {
            
            try
            {
                _logger.LogInformation("Iniciando criação de novo Software.");
                var software = await _softwareService.CreateNewSoftware(dto);
                _logger.LogInformation("Software criado com sucesso.");
                return CreatedAtAction(nameof(GetById), new { id = software.IdSoftware }, software);
            
            }catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar novo Software.");
                return StatusCode(500, "Erro interno do servidor");
            }

        }

        [HttpPut("UpdateSoftware/{id}")]
        public async Task<IActionResult> UpdateSoftware(int id, AtualizarSoftwareDTO dto)
        {
            try
            {
                _logger.LogInformation("Iniciando atualização do Software com ID {Id}.", id);
                var existingSoftware = await _softwareService.GetByIdAsync(id);
                if (existingSoftware == null)
                {
                    _logger.LogWarning("Software com ID {Id} não encontrado para atualização.", id);
                    return NotFound();
                }
                // Atualiza os campos do software existente com os valores do DTO
              
                var atualizado = await _softwareService.AtualizarAsync(id, dto);                
                _logger.LogInformation("Software com ID {Id} atualizado com sucesso.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar o Software com ID {Id}.", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpDelete("DeleteSoftware/{id:int}")]
        public async Task<IActionResult> DeleteSoftware(int id)
        {
            try
            {
                _logger.LogInformation("Iniciando exclusão do Software com ID {Id}.", id);
                var existingSoftware = await _softwareService.GetByIdAsync(id);
                if (existingSoftware == null)
                {
                    _logger.LogWarning("Software com ID {Id} não encontrado para exclusão.", id);
                    return NotFound();
                }
                var deletado = await _softwareService.DeletarAsync(id);
                if (!deletado)
                {
                    _logger.LogWarning("Falha ao excluir o Software com ID {Id}.", id);
                    return StatusCode(500, "Erro ao excluir o software.");
                }
                _logger.LogInformation("Software com ID {Id} excluído com sucesso.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir o Software com ID {Id}.", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
}
