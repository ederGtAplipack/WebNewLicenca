using AutoMapper;
using LicencaApi.Data;
using LicencaApi.DTOs;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

/*A classe LicencaService atua como intermediária entre a camada de repositório (que interage com o banco de dados) e a camada de apresentação (que consome os DTOs). 
 * Ela encapsula a lógica de negócios, garantindo que os dados sejam manipulados corretamente antes de serem enviados ou recebidos pela aplicação.*/
namespace LicencaApi.Services
{
    public class LicencaService : ILicencaService
    {
        private readonly ILicencaRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<LicencaService> _logger;
        private readonly LicencaDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public LicencaService(ILicencaRepository repository, IMapper mapper, ILogger<LicencaService> logger, LicencaDbContext context, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<LicencaModel>> BuscarTodasAsync()
        {
            _logger.LogInformation("Passando pelo LicencaService");
            var licencas = await _repository.BuscarTodasAsync();
            _logger.LogInformation("Retornando todas as licenças");
            return licencas.Select(l => _mapper.Map<LicencaModel>(l));
        }

        public async Task<IEnumerable<LicencaDetalhadaDTO>> ObterTodasComDetalhesAsync()
        {
            _logger.LogInformation("Passando pelo Service do ObterTodasComDetalhesAsync.");
            var licencasDetalhadas = await _repository.ObterTodasComDetalhesAsync();
            return licencasDetalhadas;
        }


        public async Task<LicencaModel?> BuscarPorIdLicenca(int id)
        {
            _logger.LogInformation("Passando pelo LicencaService");
            var Licencas = await _repository.BuscarPorIdAsync(id);
            if (Licencas == null)
            {
                _logger.LogWarning("Licença com ID {id} não encontrada", id);
                return null;
            }
            return Licencas;

            /*var licenca = await _repository.BuscarPorIdAsync(id);
            return licenca == null ? null : _mapper.Map<LicencaModel>(licenca);*/
        }
        public async Task<IEnumerable<LicencaModel?>> BuscarAtivasAsync()
        {
            _logger.LogInformation("Passando por Service das ativas");
            var licencasAtivas = await _repository.BuscarAtivasAsync();
            if (licencasAtivas == null || !licencasAtivas.Any())
            {
                _logger.LogWarning("Nenhuma licença ativa encontrada");
                return Enumerable.Empty<LicencaModel>();
            }
            _logger.LogInformation("Retornando {count} licenças ativas", licencasAtivas.Count());
            /*return await _context.Licenca
                .Where(l => l.Attivo)
                .ToListAsync();         */
            return licencasAtivas;
        }

        /*Esse método tem a responsabilidade de criar (inserir) uma nova licença no banco de dados a partir dos dados recebidos (DTO).*/
        public async Task<LicencaModel> CriarAsync(CriarLicencaDTO dto)
        {
            _logger.LogInformation("Iniciando a criação de uma nova licença a partir de um DTO.");

            var clienteExiste = await _context.Anagrafica.AnyAsync(c => c.IdAnagrafica == dto.IdCliente);

            if (!clienteExiste)
            {
                _logger.LogError("Cliente com ID {IdCliente} não encontrado.", dto.IdCliente);
                throw new KeyNotFoundException($"Cliente com ID {dto.IdCliente} não encontrado.");
            }

            // 1. Mapear o DTO para o modelo de domínio.
            // O Mapper já deve ter sido configurado para lidar com essa conversão.
            var licenca = _mapper.Map<LicencaModel>(dto);

            if (licenca == null)
            {
                _logger.LogError("Erro de mapeamento: O AutoMapper retornou um objeto nulo para o DTO.");
                throw new InvalidOperationException("Não foi possível mapear o DTO para o modelo de licença.");
            }

            try
            {
                // 2. Usar o repositório para adicionar a nova licença.
                await _repository.CriarAsync(licenca);

                // 3. Completar a unidade de trabalho para salvar as alterações no banco de dados.
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Licença criada com sucesso. ID da Licença: {NumLic}", licenca.NumLic);
                return licenca;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao salvar a licença no banco de dados.");

                // Re-lança a exceção para que o Controller possa capturá-la.
                throw;
            }
        }        

        public async Task<bool> AtualizarAsync(int id, AtualizarLicencaDTO dto)
        {
            _logger.LogInformation("Passando pelo Service de atualização de licença");
            var licenca = await _unitOfWork.Licencas.BuscarPorIdAsync(id);

            if (licenca == null)
            {
                _logger.LogWarning("Licença com ID {Id} não encontrada para atualização.", id);
                return false;
            }
            try
            {
                _mapper.Map(dto, licenca);
                _unitOfWork.Licencas.AtualizarAsync(licenca);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Erro de concorrência ao atualizar a licença com ID {Id}.", id);
                // O repositório não tem o método LicencaExists, o Service tem acesso ao UnitOfWork
                // para buscar a licença
                if (await _unitOfWork.Licencas.BuscarPorIdAsync(id) == null)
                {
                    return false;
                }
                throw; // Relança a exceção de concorrência para o Controller
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao mapear DTO para LicencaModel");
                throw;
            }
        }


        public async Task<bool> DesativarAsync(int id)
        {
            var licenca = await _context.Licenca.FindAsync(id);
            if (licenca == null)
                return false;
            licenca.Attivo = false;
            await _context.SaveChangesAsync();


            return true;

        }

        public async Task<bool> DeletarAsync(int id)
        {
            _logger.LogInformation("Iniciando deleção de licença com ID {Id}", id);
            var licenca = await _context.Licenca.FindAsync(id);
            if (licenca == null)
                return false;
            _context.Licenca.Remove(licenca);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> LicencaExists(int id)
        {
            return await _context.Licenca.AnyAsync(e => e.NumLic == id);
        }

        public async Task<AtivacaoDispositivoResponseDTO> ProcessarAtivacaoDispositivoAsync(AtivacaoDispositivoRequestDTO request)
        {
            // Validação dos dados de entrada
            await RegistrarAcesso(request);

            var licencaExistente = await BuscarLicencaExistenteAsync(request);
            if (licencaExistente != null && licencaExistente.NumLic > 0)
            {
                AtualizarDadosDoDispositivo(licencaExistente, request);

                if (licencaExistente.Attivo && licencaExistente.Scade > DateTime.Now)
                {
                    await _unitOfWork.CompleteAsync();
                    return GerarResposta(licencaExistente, "Ativa", "Licença já está ativa e válida.");
                }
                else
                {
                    return await RenovarLicencaAsync(licencaExistente);
                }
            }

            //return await RegistrarNovoDispositivoAsync(request);
            return GerarErro("Dispositivo não registrado. Contate o suporte.", "Dispositivo Não Registrado");
        }

        private void AtualizarDadosDoDispositivo(LicencaModel licencaExistente, AtivacaoDispositivoRequestDTO request)
        {
            licencaExistente.Processador = request.Processador;
            licencaExistente.NomeComputador = request.NomeComputador;
            licencaExistente.SistemaOp = request.SistemaOp;
            licencaExistente.ip = request.ip;
            licencaExistente.MacAddress = request.MacAddress;
            licencaExistente.Software = request.nSoftware;
            licencaExistente.DataAtivacao = DateTime.Now;

        }

        private async Task<AtivacaoDispositivoResponseDTO> RegistrarAcesso(AtivacaoDispositivoRequestDTO request)
        {
            var acessoNew = new AcessoNewModel
            {
                macaddress = request.MacAddress,
                software = request.nSoftware,
                processador = request.Processador,
                idLicenca = request.IdLicenca,
                idAcessosNew = request.idAcessosNew,
                DataHora = request.DateTime,
                externalIP = request.externalIp,
            };

            _context.AcessosNew.Add(acessoNew);
            await _context.SaveChangesAsync();

            return new AtivacaoDispositivoResponseDTO
            {
                StatusLicenca = "Registrado",
                Mensagem = "Acesso registrado com sucesso."
            };
        }


        private async Task<LicencaModel?> BuscarLicencaExistenteAsync(AtivacaoDispositivoRequestDTO request)
        {
            var licenca = await _context.Licenca
                .FirstOrDefaultAsync(l => l.MacAddress == request.MacAddress && l.Software == request.nSoftware);
            _logger.LogInformation("Verificacao: Licenca existente para MAC {mac} = {resultado}", request.MacAddress, licenca != null);
            return licenca;
        }

        private async Task<AtivacaoDispositivoResponseDTO> RenovarLicencaAsync(LicencaModel licenca)
        {
            licenca.Attivo = true;
            licenca.DataAtivacao = DateTime.Now;
            await _unitOfWork.CompleteAsync();

            return GerarResposta(licenca, "Renovada", "Licença renovada com sucesso.");
        }

        /*private async Task<AtivacaoDispositivoResponseDTO> RegistrarNovoDispositivoAsync(AtivacaoDispositivoRequestDTO request)
        {
            var contrato = await ObterContratoValidoAsync(request.IdCliente);
            if (contrato == null)
            {
                _logger.LogWarning("Contrato inválido ou inexistente para o cliente {IdCliente}.", request.IdCliente);
                return GerarErro("Contrato inválido ou inexistente para o cliente.", "Erro");
            }

            var cliente = await _context.Anagrafica.FindAsync(request.IdCliente);
            if (cliente == null)
                return GerarErro("Cliente não encontrado.", "Erro");

            var softwareDb = await _context.Software.FirstOrDefaultAsync(s => s.nSoftware == request.nSoftware);
            if (softwareDb == null)
                return GerarErro("Software não encontrado no banco de dados.", "Erro");

            var novaLicenca = _mapper.Map<LicencaModel>(request);
            novaLicenca.DataLic = DateTime.Now;
            novaLicenca.DataAtivacao = DateTime.Now;
            novaLicenca.Attivo = false;
            novaLicenca.Status = "Pendente Analise";
            novaLicenca.IdRevenda = cliente.IdRevenda ?? 0;
            novaLicenca.Scade = contrato.PagmentoEmDia
                ? DateTime.Now.AddYears(1)
                : DateTime.Now.AddDays(15);

            if (!contrato.PagamentoEmDia || contrato.StatusContrato == "Suspenso")
            {
                novaLicenca.Status = "Pendente Financeiro";
                novaLicenca.Scade = DateTime.Now.AddDays(15);
                return GerarErro("Contrato suspenso ou pagamento em atraso. Licença pendente de análise comercial.", "Pendente Financeiro");
            }

            var totalLicencasAtivas = await _context.Licenca.CountAsync(l => l.IdCliente == request.IdCliente && l.Attivo && l.Scade > DateTime.Now);

            if (totalLicencasAtivas < contrato.QtdLicencas)
            {
                novaLicenca.Attivo = true;
                novaLicenca.Status = "Ativa";
                novaLicenca.Scade = DateTime.Now.AddYears(1);
            }
            else
            {
                novaLicenca.Status = "Pendente Analise";
                novaLicenca.Scade = DateTime.Now.AddDays(30);
            }

            await _unitOfWork.Licencas.CriarAsync(novaLicenca);
            await _unitOfWork.CompleteAsync();

            var novaChave = new LicencasChaveModel
            {
                Chave = GerarNovaChaveLicenca(request),
                IdSoftware = softwareDb.IdSoftware,
                IdRevenda = cliente.IdRevenda ?? 0,
                NumLic = novaLicenca.NumLic,
                DataInser = DateTime.Now,
                TipoLic = novaLicenca.TipoLic ?? "INDEFINIDO",
                Entregue = 0,
                EntreguePara = cliente.NomeFantasia ?? cliente.RazaoSocial
            };

            _context.LicencasChave.Add(novaChave);
            await _context.SaveChangesAsync();

            novaLicenca.IdLicencaChave = novaChave.IdLicencaChave;
            await _unitOfWork.CompleteAsync();

            return new AtivacaoDispositivoResponseDTO
            {
                ChaveLicenca = novaChave.Chave,
                DataExpiracao = novaLicenca.Scade,
                StatusLicenca = novaLicenca.Status,
                Mensagem = novaLicenca.Status == "Ativa"
                            ? "Licença ativada com sucesso."
                            : "Dispositivo novo. A solicitação está sob análise comercial. Uma chave foi emitida."
            };
        }*/

        private object GerarNovaChaveLicenca(AtivacaoDispositivoRequestDTO request)
        {
            return Guid.NewGuid().ToString();
        }

        private async Task<ContratoModel> ObterContratoValidoAsync(int idCliente)
        {
            return await _context.Contratos
            .FirstOrDefaultAsync(c => c.IdCliente == idCliente &&
            c.DataInicio <= DateTime.Today &&
            (c.DataFim == null || c.DataFim >= DateTime.Today));
        }

        private AtivacaoDispositivoResponseDTO GerarErro(string mensagem, string status)
        {
            return new AtivacaoDispositivoResponseDTO
            {
                ChaveLicenca = "",
                DataExpiracao = DateTime.MinValue,
                StatusLicenca = status,
                Mensagem = mensagem
            };
        }

        private AtivacaoDispositivoResponseDTO GerarResposta(LicencaModel licenca, string status, string mensagem)
        {
            return new AtivacaoDispositivoResponseDTO
            {
                ChaveLicenca = licenca.IdLicencaChave?.ToString() ?? string.Empty,
                DataExpiracao = licenca.Scade,
                StatusLicenca = status,
                Mensagem = mensagem
            };
        }

        Task<AtivacaoDispositivoResponseDTO> ILicencaService.BuscarLicencaExistenteAsync(AtivacaoDispositivoRequestDTO request)
        {
            throw new NotImplementedException();
        }

        /*METODO REMOVIDO PARA O CLIENTESERVICE
        public async Task<IEnumerable<AnagraficaModel>> BuscarTodasAnagrafica()
        {
            _logger.LogInformation("Passando pelo LicencaService");
            var anagrafica = await _repository.BuscarTodasAnagrafica();
            _logger.LogInformation("Retornando todos Clientes");
            return anagrafica.Select(l => _mapper.Map<AnagraficaModel>(l));
        }*/

        /*METODO REMOVIDO PARA O CLIENTESERVICE*/
        /*public async Task<AnagraficaModel?> BuscarPorIdAnagrafica(int id)
        {
            _logger.LogInformation("Passando pelo LicencaService");
            var anagrafica = await _repository.BuscarPorIdAnagrafica(id);
            if (anagrafica == null)
            {
                _logger.LogWarning("Licença com ID {id} não encontrada", id);
                return null;
            }
            return anagrafica;
        }*/

        /*METODO REMOVIDO PARA O CLIENTESERVICE
        public async Task<AnagraficaModel?> CriarNovaAnagrafica(CriarAnagraficaDTO dto)
        {
            _logger.LogWarning("Iniciando Método CriarNovaAnagrafica");
            var clienteExiste = await _context.Anagrafica.AnyAsync(c => c.IdAnagrafica == dto.IdAnagrafica);
            if (clienteExiste)
            {
                _logger.LogError("Cliente com ID {IdCliente} já existe.", dto.IdAnagrafica);
                throw new InvalidOperationException($"Cliente com ID {dto.IdAnagrafica} já existe.");
            }
            var anagrafica = _mapper.Map<AnagraficaModel>(dto);


            if (anagrafica == null)
            {
                _logger.LogError("Erro de mapeamento: O AutoMapper retornou um objeto nulo para o DTO.");
                throw new InvalidOperationException("Não foi possível mapear o DTO para o modelo Anagrafica.");
            }

            try
            {
                await _repository.CreateNewAnagrafica(anagrafica);
       
                await _unitOfWork.CompleteAsync();

                //await _context.SaveChangesAsync();
                _logger.LogInformation("Cliente criado com sucesso. ID do Cliente: {IdAnagrafica}", anagrafica.IdAnagrafica);

                return anagrafica;

            }catch (Exception ex){
                _logger.LogError(ex, "Ocorreu um erro ao salvar a Anagrafica no banco de dados.");
                // Re-lança a exceção para que o Controller possa capturá-la.
                throw;
            }
        }*/

        public async Task<LicencaModel?> CriarNewLicenca(LicencaDetalhadaDTO dto)
        {
            _logger.LogInformation("Iniciando criação de licença complexa");

            // Validar cliente
            var clienteExiste = await _context.Anagrafica.AnyAsync(c => c.IdAnagrafica == dto.IdCliente);
            if (!clienteExiste)
                throw new InvalidOperationException($"Cliente {dto.IdCliente} não encontrado");

            // Mapear DTO → Model
            var model = _mapper.Map<LicencaModel>(dto);

            // Chamar repositório
            await _repository.CreateSql(model);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Licença complexa criada com ID {NumLic}", model.NumLic);

            return model;
        }

    }
}
