using Microsoft.AspNetCore.Mvc;
using LicencaApi.Models;
using LicencaApi.Services;
using LicencaApi.DTOs;

namespace LicencaApi.Controllers
{
    [ApiController]
    [Route("api/licencas")]
    public class LicencaController : ControllerBase
    {
        private readonly ILicencaService _service;
        private readonly ILogger<LicencaController> _logger;

        public LicencaController(ILicencaService service, ILogger<LicencaController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var licencas = await _service.BuscarTodasAsync();
                return Ok(licencas);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar licenças");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var licenca = await _service.BuscarPorIdAsync(id);
            if (licenca == null)
                return NotFound();

            return Ok(licenca);
        }

        [HttpGet("ativas")]
        public async Task<IActionResult> GetAtivas()
        {
            var licencas = await _service.BuscarAtivasAsync();
            return Ok(licencas);
        }      

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarLicencaDTO dto)
        {
            try
            {
                var novaLicenca = await _service.CriarAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = novaLicenca.NumLic }, novaLicenca);            
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar licença");
                return StatusCode(500, "Erro interno do servidor");
            }
            
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarLicencaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.NumLic)
                return BadRequest("ID de licença inválido.");

            var atualizado = await _service.AtualizarAsync(id, dto);
            if (!atualizado)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("{id:int}/desativar")]
        public async Task<IActionResult> Desativar(int id)
        {
            var desativado = await _service.DesativarAsync(id);
            if (!desativado)
                return NotFound();

            return Ok(new { mensagem = "Licença desativada com sucesso", id });
        }

        [HttpPost("ativar")]
        /*O parâmetro request é do tipo AtivacaoDispositivoRequestDTO, 
         * que provavelmente é uma classe definida para representar os dados que o cliente enviará no corpo da requisição (body).*/
        public async Task<IActionResult> AtivarDispositivo([FromBody] AtivacaoDispositivoRequestDTO request)
        {
            var response = await _service.ProcessarAtivacaoDispositivoAsync(request);
            return Ok(response);
        }

    }
}
