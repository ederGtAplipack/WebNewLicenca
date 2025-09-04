using LicencaApi.Data;
using LicencaApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LicencaApi.Controllers.V1
{
    public class LicencaComplexController : ControllerBase
    {
        private readonly ILicencaService _service;
        private readonly ILogger<LicencaComplexController> _logger;
        private readonly LicencaDbContext _context;
        public LicencaComplexController(ILicencaService service, ILogger<LicencaComplexController> logger, LicencaDbContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }

        [HttpPost("CreateNewLin")]
        public async Task<IActionResult> CreateNewLin([FromBody] LicencaDetalhadaDTO dto)
        {
            try
            {
                // Verificação inicial do modelo
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _logger.LogInformation("Iniciando a criação de nova licença.");
                var novaLicenca = await _service.CriarNewLicenca(dto);
                // O retorno CreatedAtAction é o padrão RESTful para criação bem-sucedida.
                _logger.LogInformation("Licença criada com sucesso: {NumLic}", novaLicenca.NumLic);                
                return CreatedAtAction(nameof(GetById), new { id = novaLicenca.NumLic }, novaLicenca);
            }
            catch (InvalidOperationException ex)
            {
                // Captura um erro específico do serviço (como falha no mapeamento) e retorna 400 Bad Request.
                _logger.LogError(ex, "Erro de validação ou operação: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                // Captura erros específicos do banco de dados (por exemplo, violação de chave primária).
                _logger.LogError(ex, "Erro de banco de dados ao criar licença.");
                return StatusCode(500, "Erro ao salvar a licença. Verifique os dados e tente novamente.");
            }
            catch (Exception ex)
            {
                // Captura qualquer outra exceção inesperada.
                _logger.LogError(ex, "Erro interno do servidor ao criar licença.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Iniciando busca por ID de licenças");
            var licenca = await _service.BuscarPorIdAnagrafica(id);
            if (licenca == null)
                return NotFound();
            _logger.LogWarning("Licença com ID {Id} encontrada", id);

            return Ok(licenca);

        }
    }
}
