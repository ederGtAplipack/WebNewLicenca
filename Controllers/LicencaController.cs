using LicencaApi.Data;
using LicencaApi.DTOs;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using LicencaApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LicencaApi.Controllers
{
    /* * O LicencaController é responsável por gerenciar as operações relacionadas às licenças.
     * Ele permite buscar, criar, atualizar e desativar licenças, além de processar ativações de dispositivos.
     * As ações são implementadas usando o padrão RESTful, com métodos HTTP apropriados.
     */
    [ApiController]
    [Route("api/licencas")]
    public class LicencaController : ControllerBase
    {
        private readonly ILicencaService _service;
        private readonly ILogger<LicencaController> _logger;
        private readonly LicencaDbContext _context;

        public LicencaController(ILicencaService service, ILogger<LicencaController> logger, LicencaDbContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }
        
        [HttpGet]      
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation("Iniciando busca por todas as licenças");
                var licencas = await _service.BuscarTodasAsync();
                //var licencas = _context.Licenca.ToList();
                _logger.LogInformation("Fim da Busca por todas licenças");
                return Ok(licencas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar licenças");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Iniciando busca por ID de licenças");
            var licenca = await _service.BuscarPorIdAsync(id);
            if (licenca == null)
                return NotFound();
            _logger.LogWarning("Licença com ID {Id} encontrada", id);

            return Ok(licenca);

        }

        [HttpGet("ativas")]
        public async Task<IActionResult> GetAtivas()
        {
            _logger.LogInformation("Iniciando busca por Ativas");
            var licencas = await _service.BuscarAtivasAsync();
            return Ok(licencas);
        }

        /*
         * O método Create é responsável por criar uma nova licença.
         * Ele recebe os dados da licença como parâmetro e chama o serviço para realizar a criação.
         * Se a licença for criada com sucesso, retorna um status 201 (Created) com a nova licença.
         * Se ocorrer algum erro, retorna um status 500 (Internal Server Error).
         */
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CriarLicencaDTO dto)
        {
            try
            {
                // Verificação inicial do modelo
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _logger.LogInformation("Iniciando a criação de nova licença.");

                var novaLicenca = await _service.CriarAsync(dto);

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

        /*
         * O método Update é responsável por atualizar uma licença existente.
         * Ele recebe o ID da licença e os dados atualizados como parâmetros.
         * Se a licença for atualizada com sucesso, retorna um status 204 (No Content).
         * Se o ID não corresponder ao ID da licença, retorna um status 400 (Bad Request).
         * Se a licença não for encontrada, retorna um status 404 (Not Found).
         */
        [HttpPut("atualizar/{id:int}")]
        public async Task<IActionResult> Update(int id, AtualizarLicencaDTO dto)
        {
            _logger.LogInformation("Iniciando atualização de licença com ID {Id}", id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            /*if (id != dto.NumLic)
                return BadRequest("ID de licença inválido.");*/

            var atualizado = await _service.AtualizarAsync(id, dto);
            if (!atualizado)
                return NotFound();

            _logger.LogInformation("Licença com ID {Id} atualizada com sucesso", id);
            return NoContent();
        }
        /*
         * O método Desativar é responsável por desativar uma licença específica.
         * Ele recebe o ID da licença como parâmetro e chama o serviço para realizar a desativação.
         * Se a licença for desativada com sucesso, retorna um status 200 (OK) com uma mensagem de sucesso.
         * Se a licença não for encontrada, retorna um status 404 (Not Found).
         */
        [HttpPatch("{id:int}/desativar")]
        public async Task<IActionResult> Desativar(int id)
        {
            var desativado = await _service.DesativarAsync(id);
            if (!desativado)
                return NotFound();

            return Ok(new { mensagem = "Licença desativada com sucesso", id });
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var licenca = await _service.BuscarPorIdAsync(id);
            if (licenca == null)
                return NotFound("Licença não encontrada."); 
            _logger.LogInformation("Iniciando deleção de licença com ID {Id}", id);
            
            var deletado = await _service.DeletarAsync(id);
            if (!deletado)
                return NotFound();
            _logger.LogInformation("Licença com ID {Id} deletada com sucesso", id);
            return Ok(new { mensagem = "Licença deletada com sucesso", id });
        }


        /*[HttpGet("buscar-existente")]
        public async Task<IActionResult> BuscarLicencaExistente([FromQuery] AtivacaoDispositivoRequestDTO request)
        {
            var response = await _service.BuscarLicencaExistenteAsync(request);
            if (request == null)
                return BadRequest("Parâmetros obrigatórios: macAddress e software.");

            var licenca = await _service.BuscarLicencaExistenteAsync(request);

            if (licenca == null)
                return NotFound("Licença não encontrada.");

            return Ok(licenca);
        }*/

    }
}
