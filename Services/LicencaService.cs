using AutoMapper;
using LicencaApi.Data;
using LicencaApi.DTOs;
using LicencaApi.Helpers;
using LicencaApi.Models;
using LicencaApi.Repositories; 
using LicencaApi.Services;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
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
        /*É necessário verificar se o cliente (IdCliente) já possui o número máximo de licenças ativas, com base no contrato da tabela contrato*/
        // Criar um novo registro na tabela `acessosnew` ou similar
        var acessoNew = new AcessoNewModel
        {
            macaddress = request.MacAddress,
            software = request.Software,
            processador = request.Processador,
            idLicenca = request.IdLicenca,
            idAcessosNew = request.idAcessosNew,
            DataHora = request.DateTime,
            externalIP = request.externalIp,

        };
        await _context.SaveChangesAsync();

        // 2. Buscar Licença Existente:
        /* Verificar se o dispositivo (MAC + Software) já possui uma licença cadastrada.*/
        // Tentar encontrar uma licença baseada no MacAddress e Software,
        // e, opcionalmente, no IdLicencaChave (serial implícita)
        var licencaExistente = await _context.Licencas
            .FirstOrDefaultAsync(l => l.MacAddress == request.MacAddress && l.Software == request.Software);

        _logger.LogInformation("Verificação: Licenca existente para MAC {mac} = {resultado}", request.MacAddress, licencaExistente != null);


        if (licencaExistente != null && licencaExistente.NumLic > 0)
        {
            //AQUI SEM TRATA COMO "LICENÇA EXISTENTE"
            // Dispositivo/Software Conhecido
            // Atualizar dados do dispositivo (IP, Processador, SO, etc.)
            licencaExistente.Ip = request.Ip;
            licencaExistente.Processador = request.Processador;
            licencaExistente.SistemaOp = request.SistemaOp; 
            licencaExistente.Tipo_Pc = request.TipoPc; // Host Type
            licencaExistente.Nome_Computador = request.NomeComputador; // Hostname
            
            // 3. Validação da Licença Existente:
            /*Licença ativa e ainda válida*/
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
                /*Licença expirada ou inativa*/
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
            // 4. Dispositivo/Software NÃO Cadastrado - Nova Instalação
            // Dispositivo/Software NÃO Cadastrado - Nova Instalação
            // 4. Cadastramento de Novo Dispositivo (Pendente de Análise Comercial):
            /*É necessário verificar se o cliente (IdCliente) já possui o número máximo de licenças ativas, com base no contrato da tabela contrato*/
            var contrato = await _context.Contratos
                .FirstOrDefaultAsync(c => c.IdCliente == request.IdCliente && c.DataInicio <= DateTime.Today && (c.DataFim == null || c.DataFim >= DateTime.Today));

            if (contrato == null) {
            // Contrato não encontrado ou inválido
                return new AtivacaoDispositivoResponseDTO
                {
                    ChaveLicenca = "",
                    DataExpiracao = DateTime.MinValue,
                    StatusLicenca = "Erro",
                    Mensagem = "Contrato inválido ou inexistente para o cliente."
                };
            }


            // 5. Criar nova licença
            var novaLicenca = _mapper.Map<LicencaModel>(request); // Mapeia DTO para Model
            novaLicenca.DataLic = DateTime.Now; // Data de registro da solicitação
            novaLicenca.DataAtivacao = DateTime.Now; // Data da primeira tentativa de ativação
            novaLicenca.Attivo = false; // Inicialmente inativa, aguardando aprovação
            novaLicenca.Status = "Pendente Analise"; // Novo status, se adicionado
            
            if (contrato != null)
            {
                var totalLicencasAtivas = await _context.Licencas
                    .CountAsync(l => l.IdCliente == request.IdCliente && l.Attivo && l.Scade > DateTime.Now);

                // Verificar se o cliente já atingiu o limite de licenças ativas
                if (totalLicencasAtivas < contrato.QtdLicencas)
                {
                    novaLicenca.Attivo = true; // Ativar licença se ainda houver espaço
                    novaLicenca.Status = "Ativa"; // Definir status como Ativa
                    novaLicenca.DataAtivacao = DateTime.Now; // Data de ativação
                    novaLicenca.Scade = DateTime.Now.AddYears(1); // Exemplo: Definir expiração para 1 ano
                }
                else
                {
                    // Cliente atingiu o limite de licenças ativas
                    novaLicenca.Attivo = false; // Manter inativa
                    novaLicenca.Status = "Pendente Analise"; // Definir status como Pendente Análise
                    novaLicenca.Scade = DateTime.Now.AddDays(30); // Exemplo: Definir expiração para 30 dias, aguardando análise comercial
                    _logger.LogWarning("Cliente {IdCliente} atingiu o limite de licenças ativas. Licença pendente de análise comercial.", request.IdCliente);
                }
                
            }
            else
            {
                // Se não houver contrato, definir como pendente de análise
                novaLicenca.Attivo = false; // Manter inativa
                novaLicenca.Status = "Pendente Analise"; // Definir status como Pendente Análise
                novaLicenca.Scade = DateTime.Now.AddDays(30); // Exemplo: Definir expiração para 30 dias, aguardando análise comercial
                _logger.LogWarning("Nenhum contrato encontrado para o cliente {IdCliente}. Licença pendente de análise comercial.", request.IdCliente);
            }


            // Gerar uma IdLicencaChave temporária ou um placeholder
            novaLicenca.IdLicencaChave = "PENDENTE_APROVACAO_" + Guid.NewGuid().ToString();

            await _unitOfWork.Licencas.CriarAsync(novaLicenca);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation("Nova licença criada para o dispositivo com MAC: {MacAddress}, com Status: {Status}, aguardando análise comercial.", request.MacAddress);
            // 5. Acionamento do Controle Comercial:
            /*Registro da nova licença no banco de dados*/

            // Enviar notificação (e-mail, sistema de tickets) para a equipe comercial
            // com os detalhes do request.
            /*Notificação e resposta final*/
            _logger.LogInformation("Nova solicitação de licença pendente de análise comercial para MAC: {MacAddress}", request.MacAddress);
            // Ex: _notificationService.SendEmailToCommercial(request);

            return new AtivacaoDispositivoResponseDTO
            {
                ChaveLicenca = novaLicenca.IdLicencaChave, // Chave temporária
                DataExpiracao = novaLicenca.Scade, // Ou uma data simbólica
                StatusLicenca = novaLicenca.Status,
                Mensagem = novaLicenca.Status == "Ativa"
                            ? "Licença ativada com sucesso."
                            : "Dispositivo novo. A solicitação está sob análise comercial. Uma chave temporária foi emitida."

            };
        }
        // Fallback (deve ser coberto pela lógica acima)
        return new AtivacaoDispositivoResponseDTO { StatusLicenca = "Erro", Mensagem = "Erro desconhecido." };
    }
    private void AtualizarDadosDoDispositivo(LicencaModel licenca, AtivacaoDispositivoRequestDTO request)
    {
        licenca.Ip = request.Ip;
        licenca.Processador = request.Processador;
        licenca.SistemaOp = request.SistemaOp;
        licenca.Tipo_Pc = request.TipoPc;
        licenca.Nome_Computador = request.NomeComputador;
    }

    private string GerarNovaChaveLicenca(AtivacaoDispositivoRequestDTO request)
    {
        /*request.MacAddress: Representa o endereço MAC do dispositivo, usado para identificar o hardware.
        request.Software: Provavelmente indica o software relacionado à ativação.
        DateTime.Now.Ticks: Retorna o número de "ticks" (unidades de tempo) desde 1º de janeiro de 0001, permitindo gerar valores únicos com base no momento atual.*/
        string rawKey = $"{request.MacAddress}-{request.Software}-{DateTime.Now.Ticks}";
        // Implementar lógica de geração de nova chave de licença
        // Exemplo simples: retornar um GUID como string
        return Guid.NewGuid().ToString();
    }

    private async Task<ContratoModel?> ObterContratoValidoAsync(int idCliente)
    {
        // Buscar contrato ativo e válido para o cliente
        return await _context.Contratos
            .FirstOrDefaultAsync(c => c.IdCliente == idCliente &&
            c.DataInicio <= DateTime.Today &&
            (c.DataFim == null || c.DataFim >= DateTime.Today));
    }

}