using LicencaApi.Controllers.V2;
using LicencaApi.Data;
using LicencaApi.DTOs;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using LicencaApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Licenses;

namespace LicencaApi.Controllers.V1
{
    /* * O LicencaController é responsável por gerenciar as operações relacionadas às licenças.
     * Ele permite buscar, criar, atualizar e desativar licenças, além de processar ativações de dispositivos.
     * As ações são implementadas usando o padrão RESTful, com métodos HTTP apropriados.
     */
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LicencaController : ControllerBase
    {
        private readonly ILogger<LicencaController> _logger;
        private readonly ILicencaService _licencaService;

        public LicencaController(ILogger<LicencaController> logger, ILicencaService licencaService)
        {
            _logger = logger;
            _licencaService = licencaService;
        }

        [HttpPost("activate")]
        public async Task<IActionResult> Activate(ActivateLicenseDTO dto)
        {
            try
            {
                var result = await _licencaService.ActivateAsync(dto);
                if (!result.Success)
                    return StatusCode(result.StatusCode, result.Message);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro Activate");
                return StatusCode(500, "Erro interno");
            }
        }
        [HttpPost("validate")]
        public async Task<IActionResult> Validate(ValidateLicenseDTO dto)
        {
            try
            {
                var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString();
                var result = await _licencaService.ValidateAsync(dto, clientIp);
                if (!result.Valid) return Unauthorized(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro em Validate");
                return StatusCode(500, "Erro interno");
            }
        }        
        [HttpPost("createNewLicenca")]
        public async Task<IActionResult> Create(CriarLicencaDTO dto)
        {
            var result = await _licencaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetDevices), new { numLic = result.NumLic }, result);
        }
        
        [HttpPut("{numLic}/status")]
        /*[Authorize(Roles = "Admin")]*/
        public async Task<IActionResult> UpdateStatus(int numLic, UpdateStatusDTO dto)
        {
            try
            {
                var ok = await _licencaService.UpdateStatusAsync(numLic, dto.Status, dto.Reason);
                if (!ok) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro em UpdateStatus");
                return StatusCode(500, "Erro interno");
            }
        }
        [HttpGet("{numLic}/devices")]
        /*[Authorize(Roles = "Admin")]*/
        public async Task<IActionResult> GetDevices(int numLic)
        {
            try
            {
                var devices = await _licencaService.GetDevicesAsync(numLic);
                return Ok(devices);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro em GetDevices");
                return StatusCode(500, "Erro interno");
            }
        }
        [HttpGet("{numLic}/logs")]
        /*[Authorize(Roles = "Admin")]*/
        public async Task<IActionResult> GetLogs(int numLic)
        {
            try
            {
                var logs = await _licencaService.GetLogsAsync(numLic);
                return Ok(logs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro em GetLogs");
                return StatusCode(500, "Erro interno");
            }
        }
    }
}
