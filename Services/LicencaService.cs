using AutoMapper;
using Humanizer;
using LicencaApi.Data;
using LicencaApi.DTOs;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using MySqlX.XDevAPI;
using NuGet.Packaging.Licenses;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Text.Json;

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

        public class ResultDevice
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public IEnumerable<LicencaDeviceDTO> Devices { get; set; }
        }

        public class DeviceExistsException : Exception
        {
            public DeviceExistsException(string message) : base(message) { }
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

        /*private object GerarNovaChaveLicenca(AtivacaoDispositivoRequestDTO request)
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
        }*/


        public async Task<ActivationResultDTO> ActivateAsync(int? numLic, ActivateLicenseDTO licenseDTO, string clienteIp = null)
        {
            // === 1) Validação do Payload ===
            if (licenseDTO == null || string.IsNullOrWhiteSpace(licenseDTO.Chave))
                return new ActivationResultDTO
                {
                    Success = false,
                    StatusCode = 400,
                    Message = "Requisição inválida: chave de licença ausente ou payload vazio."
                };

            // === 2) Buscar chave ===
            var chave = await _repository.GetLicencaChaveByChaveAsync(licenseDTO.Chave);
            if (chave == null)
            {
                await LogActivationAttemptAsync(null, licenseDTO, 404, clienteIp, "Chave não encontrada");
                return new ActivationResultDTO
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Chave não encontrada."
                };
            }

            // === 3) Buscar licença associada ===
            var licenca = await _repository.GetLicencaByIdLicencaChaveAsync(chave.IdLicencaChave);
            if (licenca == null)
            {
                await LogActivationAttemptAsync(null, licenseDTO, 404, clienteIp, "Licença não encontrada para a chave.");
                return new ActivationResultDTO
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Licença não encontrada para a chave informada."
                };
            }

            // === 4) Validações de status e expiração ===
            if (licenca.Scade < DateTime.UtcNow)
            {
                await LogActivationAttemptAsync(licenca.NumLic, licenseDTO, 403, clienteIp, "Licença expirada.");
                return new ActivationResultDTO
                {
                    Success = false,
                    StatusCode = 403,
                    Message = "Licença expirada."
                };
            }

            if (licenca.Status == "Revoked" || licenca.Status == "Suspended")
            {
                await LogActivationAttemptAsync(licenca.NumLic, licenseDTO, 403, clienteIp, $"Licença está {licenca.Status}.");
                return new ActivationResultDTO
                {
                    Success = false,
                    StatusCode = 403,
                    Message = $"Licença não pode ser ativada. Status atual: {licenca.Status}."
                };
            }

            // === 5) Se já estiver ativa ===
            if (licenca.Status == "Active" && licenca.Attivo == 1)
            {
                var existing = await _repository.GetDeviceByFingerprintAsync(licenca.NumLic, licenseDTO.DeviceFingerprint);
                if (existing != null)
                {
                    var device = new LicencaDispositivoModel
                    {
                        numLic = licenca.NumLic,
                        DeviceFingerprint = licenseDTO.DeviceFingerprint,
                        DeviceInfo = licenseDTO.DeviceInfo != null ? JsonSerializer.Serialize(licenseDTO.DeviceInfo) : null,
                        ActivatedAt = DateTime.UtcNow,
                        LastSeenAt = DateTime.UtcNow,
                        IsActive = 1
                    };
                    await _repository.AddDeviceAsync(device);
                }

                await LogActivationAttemptAsync(licenca.NumLic, licenseDTO, 200, clienteIp, "Licença já ativa.");
                return new ActivationResultDTO
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Licença já está ativa e vinculada ao dispositivo.",
                    NumLic = licenca.NumLic,
                    ExpiresAt = licenca.Scade,
                    RemainingSlots = Math.Max(0, licenca.MaxDevices - await _repository.CountActiveDevicesAsync(licenca.NumLic))
                };
            }

            // === 6) Ativação de licença pendente ===
            if (licenca.Status == "PendingActivation")
            {
                var count = await _repository.CountActiveDevicesAsync(licenca.NumLic);
                var max = licenca.MaxDevices > 0 ? licenca.MaxDevices : 1;
                var existing = await _repository.GetDeviceByFingerprintAsync(licenca.NumLic, licenseDTO.DeviceFingerprint);

                if (existing == null && count >= max)
                {
                    await LogActivationAttemptAsync(licenca.NumLic, licenseDTO, 403, clienteIp, "Limite de dispositivos atingido.");
                    return new ActivationResultDTO
                    {
                        Success = false,
                        StatusCode = 403,
                        Message = "Limite máximo de dispositivos atingido para esta licença."
                    };
                }

                LicencaDispositivoModel device;

                if (existing == null)
                {
                    // Cria novo registro de dispositivo
                    device = new LicencaDispositivoModel
                    {
                        numLic = licenca.NumLic,
                        DeviceFingerprint = licenseDTO.DeviceFingerprint,
                        DeviceInfo = JsonSerializer.Serialize(licenseDTO),
                        //DeviceInfo = licenseDTO.DeviceInfo != null ? JsonSerializer.Serialize(licenseDTO.DeviceInfo) : null,
                        ActivatedAt = DateTime.UtcNow,
                        LastSeenAt = DateTime.UtcNow,
                        IsActive = 1
                    };
                    await _repository.AddDeviceAsync(device);
                }
                else
                {
                    // Atualiza dispositivo existente
                    existing.LastSeenAt = DateTime.UtcNow;
                    existing.DeviceInfo = licenseDTO.DeviceInfo != null
                        ? JsonSerializer.Serialize(licenseDTO.DeviceInfo)
                        : existing.DeviceInfo;
                    existing.IsActive = 1;
                    await _repository.UpdateDeviceAsync(existing);
                    device = existing;
                }

                // Atualiza a licença
                licenca.Attivo = 1;
                licenca.Status = "Active";
                licenca.DataAtivacao = DateTime.UtcNow;
                await _repository.UpdateLicencaAsync(licenca);

                await LogActivationAttemptAsync(licenca.NumLic, licenseDTO, 200, clienteIp, "Licença ativada com sucesso.");

                var remaining = Math.Max(0, max - (existing == null ? count + 1 : count));
                return new ActivationResultDTO
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Licença ativada com sucesso.",
                    NumLic = licenca.NumLic,
                    ExpiresAt = licenca.Scade,
                    RemainingSlots = remaining
                };
            }

            // === 7) Fallback: status inesperado ===
            await LogActivationAttemptAsync(licenca.NumLic, licenseDTO, 403, clienteIp, "Status de licença inválido para ativação.");
            return new ActivationResultDTO
            {
                Success = false,
                StatusCode = 403,
                Message = $"Licença não pode ser ativada no estado atual: {licenca.Status ?? "indefinido"}."
            };
        }


        // ==============================================
        // MÉTODO AUXILIAR CENTRALIZADO DE LOG
        // ==============================================
        private async Task LogActivationAttemptAsync(int? numLic, ActivateLicenseDTO dto, int code, string ip, string message)
        {
            await _repository.LogAsync(new LicencaLogModel
            {
                numLic = numLic,
                chave = dto?.Chave,
                endPoint = "activate",
                RequestPayload = dto != null ? JsonSerializer.Serialize(dto) : null,
                responseCode = code,
                ClienteIp = ip,
                createdAt = DateTime.UtcNow
            });
        }


        public async Task<ValidationResultDTO> ValidateAsync(ValidateLicenseDTO licenseDTO, string clienteIp = null)
        {
            if (licenseDTO == null || string.IsNullOrWhiteSpace(licenseDTO.Chave))
                return new ValidationResultDTO
                {
                    Valid = false,
                    Message = "Payload inválido"
                };
            var chave = await _repository.GetLicencaChaveByChaveAsync(licenseDTO.Chave);
            if (chave == null)
            {
                await _repository.LogAsync(new LicencaLogModel
                {
                    numLic = null,
                    chave = licenseDTO.Chave,
                    endPoint = "validate",
                    RequestPayload = JsonSerializer.Serialize(licenseDTO),
                    responseCode = 404,
                    ClienteIp = clienteIp
                });
                return new ValidationResultDTO
                {
                    Valid = false,
                    Message = "Chave não encontrada"
                };
            }

            var licenca = await _repository.GetLicencaByIdLicencaChaveAsync(chave.IdLicencaChave);
            if (licenca == null)
            {
                await _repository.LogAsync(new LicencaLogModel
                {
                    numLic = null,
                    chave = licenseDTO.Chave,
                    endPoint = "validate",
                    RequestPayload = JsonSerializer.Serialize(licenseDTO),
                    responseCode = 404,
                    ClienteIp = clienteIp
                });
                return new ValidationResultDTO
                {
                    Valid = false,
                    Message = "Licença não encontrada para a chave"
                };
            }

            // verifica se a licença está ativa e não expirada
            if (!string.Equals(licenca.Status, "Active", StringComparison.OrdinalIgnoreCase))
            {
                await _repository.LogAsync(new LicencaLogModel
                {
                    numLic = licenca.NumLic,
                    chave = licenseDTO.Chave,
                    endPoint = "validate",
                    RequestPayload = JsonSerializer.Serialize(licenseDTO),
                    responseCode = 403,
                    ClienteIp = clienteIp
                });
                return new ValidationResultDTO
                {
                    Valid = false,
                    Message = $"Licença está {licenca.Status}"
                };
            }
            if (licenca.Scade < DateTime.UtcNow)
            {
                await _repository.LogAsync(new LicencaLogModel
                {
                    numLic = licenca.NumLic,
                    chave = licenseDTO.Chave,
                    endPoint = "validate",
                    RequestPayload = JsonSerializer.Serialize(licenseDTO),
                    responseCode = 403,
                    ClienteIp = clienteIp
                });
                return new ValidationResultDTO
                {
                    Valid = false,
                    Status = "Expired",
                    Message = "Licença expirada"
                };
            }

            var device = await _repository.GetDeviceByFingerprintAsync(licenca.NumLic, licenseDTO.DeviceFingerprint);
            if (device == null || device.IsActive == 0)
            {
                await _repository.LogAsync(new LicencaLogModel
                {
                    numLic = licenca.NumLic,
                    chave = licenseDTO.Chave,
                    endPoint = "validate",
                    RequestPayload = JsonSerializer.Serialize(licenseDTO),
                    responseCode = 401,
                    ClienteIp = clienteIp
                });
                return new ValidationResultDTO
                {
                    Valid = false,
                    Message = "Dispositivo não Ativo"
                };
            }

            // atualiza último acesso
            device.LastSeenAt = DateTime.UtcNow;
            await _repository.UpdateDeviceAsync(device);

            // log seccess
            await _repository.LogAsync(new LicencaLogModel
            {
                numLic = licenca.NumLic,
                chave = licenseDTO.Chave,
                endPoint = "validate",
                RequestPayload = JsonSerializer.Serialize(licenseDTO),
                responseCode = 200,
                ClienteIp = clienteIp
            });
            return new ValidationResultDTO
            {
                Valid = true,
                Message = "Licença válida",
                Status = licenca.Status ?? "Active",
                ExpiresAt = licenca.Scade
            };
        }

        public async Task<LicencaDTO> CreateAsync(CriarLicencaDTO dto)
        {
            // 1) Validações iniciais
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (dto.IdSoftware <= 0) throw new ArgumentException("IdSoftware inválido.", nameof(dto.IdSoftware));
            if (dto.IdCliente == null || dto.IdCliente <= 0) throw new ArgumentException("IdCliente inválido.", nameof(dto.IdCliente));

            // Obtem contrato ativo e válido do cliente (inclui verificação de existência do cliente)
            var contrato = await _context.Contratos
                .FirstOrDefaultAsync(c =>
                    c.IdCliente == dto.IdCliente &&
                    c.StatusContrato == "Ativo" &&
                    c.PagamentoEmDia == 1);

            if (contrato is null)
            {
                bool clienteExiste = await _context.Anagrafica
                    .AnyAsync(a => a.IdAnagrafica == dto.IdCliente);

                string mensagem = clienteExiste
                    ? $"Cliente {dto.IdCliente} não possui contrato ativo, pago e válido."
                    : $"Cliente {dto.IdCliente} não encontrado.";

                throw new InvalidOperationException(mensagem);
            }

            if (contrato.QtdLicencas <= 0)
            {
                // Cria log da tentativa
                await _repository.LogAsync(new LicencaLogModel
                {
                    numLic = null,
                    chave = null,
                    endPoint = "create",
                    RequestPayload = JsonSerializer.Serialize(dto),
                    responseCode = 409,
                    ClienteIp = null,
                    createdAt = DateTime.UtcNow,
                    mensagem = $"Contrato #{contrato.IdContrato} do cliente {dto.IdCliente} não possui licenças disponíveis."
                });

                // Retorno amigável ao cliente
                return new LicencaDTO
                {
                    NumLic = 0,
                    Status = "Error",
                    Chave = null,
                    Scade = DateTime.UtcNow,
                    Mensagem = $"O contrato #{contrato.IdContrato} do cliente {dto.IdCliente} não possui licenças disponíveis."
                };
            }

            // === Preparação das entidades (não salvamos ainda — tudo dentro da transação abaixo) ===

            var chave = CriarLicencaChaveModel(dto, contrato);

            var scade = CalcularScade(dto.Scade, dto.DataLic, contrato.DataFim, contrato.Plano);

            var licenca = CriarLicencaModel(dto, contrato, chave, scade);


            // === Operação transacional: inserir chave, licença, decrementar contrato e atualizar chave com NumLic ===
            using (var tx = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Adiciona a chave (sem salvar se o repositório seguir padrão UnitOfWork)
                    await _repository.CreateLicencaChaveAsync(chave); // deve apenas Add() no contexto

                    // Garanta que a chave está com Id gerado após SaveChanges (se CreateLicencaChaveAsync salvar, ok).
                    // Se CreateLicencaChaveAsync não salvar, a chave terá Id depois do CompleteAsync abaixo.

                    // Vincula a chave à licença (IdLicencaChave pode estar preenchido ou será preenchido após o Save)
                    licenca.IdLicencaChave = chave.IdLicencaChave;

                    // Adiciona licença
                    await _repository.CreateLicencaAsync(licenca); // deve apenas Add() no contexto

                    // Decrementa qtdLicencas do contrato
                    _logger.LogInformation("Decrementando QtdLicencas do contrato ID {IdContrato}. Antes: {QtdAntes}", contrato.IdContrato, contrato.QtdLicencas);
                    contrato.QtdLicencas = Math.Max(0, contrato.QtdLicencas - 1);
                    _context.Contratos.Update(contrato);

                    // Se o Id da licença foi gerado (licenca.NumLic), atualiza a chave
                    chave.NumLic = licenca.NumLic;
                    _context.LicencasChave.Update(chave);

                    // Persiste tudo via UnitOfWork (atomicamente)
                    await _unitOfWork.CompleteAsync();

                    // Confirma transação
                    await tx.CommitAsync();
                }
                catch
                {
                    await tx.RollbackAsync();
                    throw;
                }
            }

            // === 5) Gravar Log e Retorno ===

            // Grava log de criação (padrão async, não bloqueante para a resposta)
            await LogCreationAsync(licenca, chave, dto);

            // Retorna DTO resumido
            return new LicencaDTO
            {
                NumLic = licenca.NumLic,
                Chave = chave.Chave,
                Scade = licenca.Scade,
                Status = licenca.Status
            };
        }

        //gera uma chave aleatória para a licença
        private string GerarChaveAleatoria()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            string bloco() => new string(Enumerable.Range(0, 5).Select(_ => chars[random.Next(chars.Length)]).ToArray());
            return $"{bloco()}-{bloco()}-{bloco()}-{bloco()}-{bloco()}";
        }


        private LicencasChaveModel CriarLicencaChaveModel(CriarLicencaDTO dto, ContratoModel contrato)
        {
            /*string clienteId = dto.IdCliente?.ToString("D5") ?? "00000";
            string tipoLic = (dto.TipoLic ?? contrato.Plano ?? "GEN").Substring(0, Math.Min(3, (dto.TipoLic ?? "GEN").Length)).ToUpper();
            string dataLic = (dto.DataLic != default(DateTime) ? dto.DataLic : DateTime.UtcNow).ToString("yyMM");
            string mac = (dto.MacAddress ?? "000000000000").Replace(":", "").Replace("-", "").ToUpper();
            mac = mac.Length > 6 ? mac.Substring(mac.Length - 6) : mac.PadLeft(6, '0');
            string nomePc = (dto.NomeComputador ?? "PCNAME").ToUpper().Substring(0, Math.Min(6, (dto.NomeComputador ?? "PCNAME").Length));
            string tipoPc = (dto.TipoPc ?? "PC").ToUpper().Substring(0, Math.Min(2, (dto.TipoPc ?? "PC").Length));
            string proc = (dto.Processador ?? "CPU").ToUpper().Substring(0, Math.Min(3, (dto.Processador ?? "CPU").Length));

            string random = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();

            string chaveString = $"LIC-{clienteId}-{tipoLic}{tipoPc}{proc}-{mac}-{random}";*/

            string chaveString = GerarChaveAleatoria();

            return new LicencasChaveModel
            {
                Chave = chaveString,
                IdSoftware = dto.IdSoftware,
                IdRevenda = dto.IdRevenda ?? null,
                NumLic = 0,
                DataInser = DateTime.UtcNow,
                TipoLic = string.IsNullOrWhiteSpace(dto.TipoLic) ? (contrato?.Plano ?? "INDEFINIDO") : dto.TipoLic,
                Entregue = 0,
                Status = "Available"
            };
        }
        private DateTime CalcularScade(DateTime dtoScade, DateTime dtoDataLic, DateTime? contratoDataFim, string plano)
        {
            // Prioridade: DTO > contrato.dataFim > cálculo por plano > 1 ano por padrão
            if (dtoScade != default(DateTime)) return dtoScade;
            if (contratoDataFim.HasValue) return contratoDataFim.Value;

            var dataBase = dtoDataLic == default(DateTime) ? DateTime.UtcNow : dtoDataLic;

            if (!string.IsNullOrWhiteSpace(plano))
            {
                var p = plano.ToLowerInvariant();
                if (p.Contains("anual") || p.Contains("ano")) return dataBase.AddYears(1);
                if (p.Contains("mensal") || p.Contains("mês")) return dataBase.AddMonths(1);
                if (p.Contains("trimestral")) return dataBase.AddMonths(3);
                // adicionar regras conforme necessário
            }

            return dataBase.AddYears(1); // fallback
        }
        private LicencaModel CriarLicencaModel(CriarLicencaDTO dto, ContratoModel contrato, LicencasChaveModel chave, DateTime scade)
        {
            var dataLic = dto.DataLic == default(DateTime) ? DateTime.UtcNow : dto.DataLic;

            // Definir MaxDevices: priorizar DTO se informado e dentro do permitido pelo contrato,
            // senão, usar valor padrão a partir do contrato (por exemplo qtdLicencas ou limite específico)
            int maxDevices;
            if (dto.MaxDevices > 0)
            {
                // Se existir uma regra no contrato sobre MaxDevicesPermitidos, aplicar aqui:
                // maxDevices = Math.Min(dto.MaxDevices, contrato.MaxDevicesPermitidos);
                maxDevices = dto.MaxDevices;
            }
            else
            {
                maxDevices = contrato.QtdLicencas > 0 ? contrato.QtdLicencas : 1;
            }

            return new LicencaModel
            {
                IdCliente = dto.IdCliente ?? 0,
                TipoLic = chave.TipoLic,
                MacAddress = dto.MacAddress,
                DataLic = dataLic,
                Scade = scade,
                MaxDevices = maxDevices,
                Attivo = 0,
                IdRevenda = dto.IdRevenda ?? 0,
                IdLicencaChave = chave.IdLicencaChave,
                Status = "PendingActivation",
                DataAtivacao = null,
                SistemaOp = dto.SistemaOp,
                TipoPc = dto.TipoPc,
                NomeComputador = dto.NomeComputador,
                Software = dto.Software,
                ip = dto.Ip,
                Processador = dto.Processador
            };
        }

        //----------------------------------------------------------
        // MÉTODOS DE LOG (CRIAÇÃO E ATIVAÇÃO)
        //----------------------------------------------------------
        private async Task LogCreationAsync(LicencaModel licenca, LicencasChaveModel chave, CriarLicencaDTO dto)
        {
            try
            {
                var log = new LicencaLogModel
                {
                    numLic = licenca.NumLic,
                    chave = chave.Chave,
                    endPoint = "create",
                    RequestPayload = JsonSerializer.Serialize(dto),
                    responseCode = 201,
                    ClienteIp = null,
                    createdAt = DateTime.UtcNow,
                    mensagem = "Licença criada com sucesso !, {numLic=NumLic}"
                };

                await _repository.LogAsync(log);
            }
            catch (Exception ex)
            {
                // Não falhar a criação da licença por falha no log; apenas registre localmente
                _logger?.LogWarning(ex, "Falha ao gravar log de criação da licença numLic={NumLic}, {licenca?.NumLic}");
            }
        } 
        
        private async Task LogCreationMultiAsync(int numLic, int idCliente, string chave, string mensagem)
        {
            try
            {
                var log = new LicencaLogModel
                {
                    numLic = numLic,
                    chave = chave,
                    endPoint = "create-multiple",
                    RequestPayload = JsonSerializer.Serialize(new { numLic, idCliente, chave }),
                    responseCode = 201,
                    ClienteIp = null,
                    createdAt = DateTime.UtcNow,
                    mensagem = mensagem
                };
                await _repository.LogAsync(log);
            }
            catch (Exception ex)
            {
                // Não falhar a criação da licença por falha no log; apenas registre localmente
                _logger?.LogWarning(ex, "Falha ao gravar log de criação da licença numLic={NumLic}", numLic);
            }
        }


        public async Task<bool> UpdateStatusAsync(int numLic, string status, string reason = null)
        {
            var licenca = await _repository.GetLicencaByNumLicAsync(numLic);
            if (licenca == null) return false;
            licenca.Status = status;

            await _repository.UpdateLicencaAsync(licenca);
            await _repository.LogAsync(new LicencaLogModel
            {
                numLic = licenca.NumLic,
                chave = licenca.IdLicencaChave?.ToString(),
                endPoint = "update-status",
                RequestPayload = JsonSerializer.Serialize(new { status, reason }),
                responseCode = 200,
                ClienteIp = null
            });
            return true;
        }

        // ==============================================
        // MÉTODOS PARA RELATÓRIOS E AUDITORIA
        // ==============================================
        public async Task<IEnumerable<LicencaDeviceDTO>> GetDevicesAsync(int numLic)
        {
            var devices = await _repository.GetDevicesAsync(numLic);

            if (devices == null || !devices.Any())
            {
                _logger.LogWarning("Nenhum dispositivo encontrado para a licença numLic={NumLic}", numLic);
                throw new DeviceExistsException($"Nenhum dispositivo encontrado para a licença {numLic}.");
            }

            var deviceDtos = devices.Select(d => new LicencaDeviceDTO
            {
                DeviceFingerprint = d.DeviceFingerprint,
                ActivatedAt = d.ActivatedAt,
                DeviceInfo = JsonSerializer.Serialize(d.DeviceInfo),
                LastSeenAt = d.LastSeenAt,
                IsActive = d.IsActive == 1
            }).ToList();

            return new ResultDevice
            {
                Success = true,
                Message = $"{deviceDtos.Count} dispositivo(s) encontrado(s) para a licença.",
                Devices = deviceDtos
            }.Devices;
        }

        public async Task<IEnumerable<LicencaLogDto>> GetLogsAsync(int numLic, DateTime? from = null, DateTime? to = null)
        {
            var logs = await _repository.GetLogsAsync(numLic, from, to);
            return logs.Select(l => new LicencaLogDto
            {
                IdLog = l.idLog,
                NumLic = l.numLic,
                Chave = l.chave,
                Endpoint = l.endPoint,
                RequestPayload = l.RequestPayload,
                ResponseCode = l.responseCode,
                ClientIp = l.ClienteIp,
                CreatedAt = l.createdAt
            });
        }

        // Adicione este método para implementar corretamente a interface ILicencaService
        public async Task<ActivationResultDTO> ActivateAsync(ActivateLicenseDTO licenseDTO, string clienteIp = null)
        {
            // Chame o método existente, passando null para numLic  
            return await ActivateAsync(null, licenseDTO, clienteIp);
        }

        public async Task<GenerateMultipleResultDTO> GenerateMultipleAsync(GenerateMultipleLicensesDTO dto, LicencaDTO licencaDTO)
        {
            var result = new GenerateMultipleResultDTO { TotalRequested = dto.Quantidade };

            // 1. Validar contrato
            var contrato = _context.Contratos
                .FirstOrDefault(c => c.IdContrato == dto.IdContrato &&
                                    c.IdCliente == dto.IdCliente &&
                                    c.StatusContrato == "Ativo" &&
                                    c.PagamentoEmDia == 1);

            if (contrato is null)
                throw new InvalidOperationException($"Contrato {dto.IdContrato} inválido para o cliente {dto.IdCliente}.");

            if (contrato.QtdLicencas < dto.Quantidade)
                throw new InvalidOperationException($"Contrato não possui licenças. Disponíveis: {contrato.QtdLicencas}.");

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                for (int i = 0; i < dto.Quantidade; i++)
                {
                    try
                    {
                        // 2. Criar chave
                        var chave = new LicencasChaveModel
                        {
                            //será gerada na Ativação da licença
                            Chave = GerarChaveAleatoria(),
                            IdSoftware = dto.IdSoftware,
                            TipoLic = contrato.Plano ?? "DEMO",
                            Status = "Available",
                            Entregue = 0,
                            DataInser = DateTime.UtcNow
                        };

                        await _repository.CreateLicencaChaveAsync(chave);

                        // 3. Criar licença
                        var licenca = new LicencaModel
                        {
                            IdCliente = dto.IdCliente,
                            IdLicencaChave = chave.IdLicencaChave,
                            TipoLic = contrato.Plano ?? "PADRAO",
                            DataLic = DateTime.UtcNow,
                            Scade = CalcularScade(dto.Scade, DateTime.UtcNow, contrato.DataFim, contrato.Plano),
                            Status = "PendingActivation",
                            Attivo = 0,
                            MaxDevices = dto.MaxDevices,
                            Software = null
                        };

                        await _repository.CreateLicencaAsync(licenca);

                        //var scade = CalcularScade(dto.Scade, licencaDTO.DataLic, contrato.DataFim, contrato.Plano);

                        // 4. Atualizar a chave com o número da licença
                        chave.NumLic = licenca.NumLic;
                        _context.LicencasChave.Update(chave);

                        result.CreatedLicenseKeys.Add(new LicencaDTO
                        {
                            NumLic = licenca.NumLic,
                            Chave = chave.Chave,
                            Scade = licenca.Scade,
                            Status = licenca.Status
                        });

                        await LogCreationMultiAsync(licenca.NumLic, dto.IdCliente, chave.Chave, "Licença gerada por GenerateMultipleAsync");

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Erro ao gerar licença {Index} de {Total} para o contrato {ContratoId}", i + 1, dto.Quantidade, dto.IdContrato);
                        result.FailedLicenseKeys.Add($"Erro ao gerar licença {i + 1}: {ex.Message}");

                    }
                }
                // 5. Atualizar saldo de contrato
                contrato.QtdLicencas = Math.Max(0, contrato.QtdLicencas - result.CreatedLicenseKeys.Count);
                _context.Contratos.Update(contrato);

                // 6. Commit
                await _unitOfWork.CompleteAsync();
                await transaction.CommitAsync();

                result.TotalCreated = result.CreatedLicenseKeys.Count;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar múltiplas licenças para o contrato {ContratoId}", dto.IdContrato);
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<GenerateMultipleResultDTO> GenerateMultipleAsync(GenerateMultipleLicensesDTO dto)
        {
            return await GenerateMultipleAsync(dto, null);
        }
       
    }
}
