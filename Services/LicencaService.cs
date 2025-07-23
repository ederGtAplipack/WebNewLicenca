using LicencaApi.Data;
using LicencaApi.DTOs;
using LicencaApi.Models;
using LicencaApi.Services;
using LicencaApi.Repositories; 
using Microsoft.EntityFrameworkCore;
using LicencaApi.Helpers;
using AutoMapper;
using System.Threading.RateLimiting;

namespace LicencaApi.Services;


public class LicencaService : ILicencaService
{
    private readonly ILicencaRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly LicencaDbContext  _context;
    private readonly IMapper _mapper;
    private readonly ILogger<LicencaService> _logger;

    public LicencaService(LicencaDbContext context, IMapper mapper, ILogger<LicencaService> logger, IUnitOfWork unitOfWork, ILicencaRepository repository)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }
    public async Task<IEnumerable<LicencaModel>> BuscarTodasAsync()
    {
        //return await _context.Licencas.ToListAsync();
        return await _repository.BuscarTodasAsync(); // Se usar repositório
    }
    public async Task<LicencaModel?> BuscarPorIdAsync(int id)
    {
        return await _context.Licencas.FindAsync(id);        
    }

    public async Task<IEnumerable<LicencaModel>> BuscarAtivasAsync()
    {
        return await _context.Licencas
            .Where(l => l.Attivo)
            .ToListAsync();
    }


    public async Task<LicencaModel> CriarAsync(CriarLicencaDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));
        
        //var licenca = dto.ToModel();
        var licenca = _mapper.Map<LicencaModel>(dto);
        await _unitOfWork.Licencas.CriarAsync(licenca);
        await _unitOfWork.CompleteAsync();
        
        // Atribuição de valores diretamente do DTO
        /*_context.Add(licenca);
        await _context.SaveChangesAsync();*/
        return licenca;
              
    }

    public async Task<bool> AtualizarAsync(int id, AtualizarLicencaDTO dto)
    {
        var licenca = await _context.Licencas.FindAsync(id);
        if (licenca == null)
            return false;

        licenca.ApplyUpdate(dto);

        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await LicencaExists(id))
                return false;
            throw;
        }
    }

    public async Task<bool> DesativarAsync(int id)
    {
        var licenca = await _context.Licencas.FindAsync(id);
        if (licenca == null)
            return false;

        licenca.Attivo = false;
        await _context.SaveChangesAsync();
        return true;
    }

    private async Task<bool> LicencaExists(int id)
    {
        return await _context.Licencas.AnyAsync(l => l.NumLic == id);
    }

    public async Task<AtivacaoDispositivoResponseDTO> ProcessarAtivacaoDispositivoAsync(AtivacaoDispositivoRequestDTO request)
    {

        // 1. Log do Acesso (Registrar cada tentativa de ativação/acesso)
        // Criar um novo registro na tabela `acessosnew` ou similar
        // _context.AcessosNew.Add(new AcessoNewModel { ... });
        // await _context.SaveChangesAsync();

        // 2. Buscar Licença Existente:
        // Tentar encontrar uma licença baseada no MacAddress e Software,
        // e, opcionalmente, no IdLicencaChave (serial implícita)
        var licencaExistente = await _context.Licencas
            .FirstOrDefaultAsync(l => l.MacAddress == request.MacAddress && l.Software == request.Software);

        if (licencaExistente != null)
        {
            // Dispositivo/Software Conhecido
            // Atualizar dados do dispositivo (IP, Processador, SO, etc.)
            // licencaExistente.Ip = request.Ip;
            // licencaExistente.Processador = request.Processador;
            // ... (outros campos)

            // 3. Validação da Licença Existente:
            if (licencaExistente.Attivo && licencaExistente.Scade > DateTime.Now)
            {
                // Licença Ativa e Válida
                await _unitOfWork.CompleteAsync(); // Salvar updates de dados do dispositivo
                return new AtivacaoDispositivoResponseDTO
                {
                    ChaveLicenca = licencaExistente.IdLicencaChave.ToString(), // Converte o int para string
                    DataExpiracao = licencaExistente.Scade,
                    StatusLicenca = "Ativa",
                    Mensagem = "Licença já está ativa e válida."
                };
            }
            else if (!licencaExistente.Attivo || licencaExistente.Scade <= DateTime.Now)
            {
                // Licença Inativa ou Expirada - Tentar Reativação/Renovação
                // Regra de Negócio: Verificar se o cliente tem direito a renovação/reativação
                // Isso pode envolver consultas a outras tabelas (contratos, clientes)
                bool elegivelRenovacao = true; // Exemplo: Substituir por lógica real

                if (elegivelRenovacao)
                {
                    // Renovar Licença
                    licencaExistente.Attivo = true;
                    licencaExistente.DataAtivacao = DateTime.Now;
                    // Calcular nova data de expiração (ex: +1 ano do TipoLic)
                    // licencaExistente.Scade = DateTime.Now.AddYears(1);
                    // Gerar nova IdLicencaChave se a renovação implicar em nova chave
                    // licencaExistente.IdLicencaChave = GerarNovaChaveLicenca(request);

                    await _unitOfWork.CompleteAsync();
                    return new AtivacaoDispositivoResponseDTO
                    {
                        ChaveLicenca = licencaExistente.IdLicencaChave.ToString(),
                        DataExpiracao = licencaExistente.Scade,
                        StatusLicenca = "Renovada",
                        Mensagem = "Licença renovada com sucesso."
                    };
                }
                else
                {
                    // Não Elegível para Renovação
                    return new AtivacaoDispositivoResponseDTO
                    {
                        ChaveLicenca = "",
                        DataExpiracao = licencaExistente.Scade,
                        StatusLicenca = "Expirada/Inativa",
                        Mensagem = "Licença expirada ou inativa e não elegível para renovação automática. Contate o comercial."
                    };
                }
            }
        }
        else
        {
            // Dispositivo/Software NÃO Cadastrado - Nova Instalação
            // 4. Cadastramento de Novo Dispositivo (Pendente de Análise Comercial):
            var novaLicenca = _mapper.Map<LicencaModel>(request); // Mapeia DTO para Model
            novaLicenca.DataLic = DateTime.Now; // Data de registro da solicitação
            novaLicenca.DataAtivacao = DateTime.Now; // Data da primeira tentativa de ativação
            novaLicenca.Attivo = false; // Inicialmente inativa, aguardando aprovação
            novaLicenca.Status = "Pendente Analise"; // Novo status, se adicionado
            // Gerar uma IdLicencaChave temporária ou um placeholder
            novaLicenca.IdLicencaChave = "PENDENTE_APROVACAO_" + Guid.NewGuid().ToString();

            await _unitOfWork.Licencas.CriarAsync(novaLicenca);
            await _unitOfWork.CompleteAsync();

            // 5. Acionamento do Controle Comercial:
            // Enviar notificação (e-mail, sistema de tickets) para a equipe comercial
            // com os detalhes do request.
            _logger.LogInformation("Nova solicitação de licença pendente de análise comercial para MAC: {MacAddress}", request.MacAddress);
            // Ex: _notificationService.SendEmailToCommercial(request);

            return new AtivacaoDispositivoResponseDTO
            {
                ChaveLicenca = novaLicenca.IdLicencaChave, // Chave temporária
                DataExpiracao = DateTime.MinValue, // Ou uma data simbólica
                StatusLicenca = "Pendente Analise",
                Mensagem = "Dispositivo não cadastrado. Sua solicitação de licença está sob análise comercial. Uma chave temporária foi emitida."
            };
        }
        // Fallback (deve ser coberto pela lógica acima)
        return new AtivacaoDispositivoResponseDTO { StatusLicenca = "Erro", Mensagem = "Erro desconhecido." };
    }
    private string GerarNovaChaveLicenca(AtivacaoDispositivoRequestDTO request)
    {
        string rawKey = $"{request.MacAddress}-{request.Software}-{DateTime.Now.Ticks}";
        // Implementar lógica de geração de nova chave de licença
        // Exemplo simples: retornar um GUID como string
        return Guid.NewGuid().ToString();
    }

}