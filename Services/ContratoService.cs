using AutoMapper;
using LicencaApi.Data;
using LicencaApi.DTOs;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel;

namespace LicencaApi.Services
{
    public class ContratoService : IContratoService
    {
        private readonly ILogger<ContratoService> _logger;
        private readonly IContratoRepository _contratoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;

        public ContratoService(IUnitOfWork unitOfWork, ILogger<ContratoService> logger, IMapper mapper, IContratoRepository repository, IAuditService auditService)
        {
            _contratoRepository = repository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _auditService = auditService;
        }

        public async Task<ContratoModel?> BuscarPorIdContrato(int id)
        {
            _logger.LogInformation("Passando pelo ContratoService BuscarPorIdContrato.");
            return await _contratoRepository.BuscarPorIdContrato(id);
        }

        public async Task<IEnumerable<ContratoDetalhadoDTO>> BuscarTodosContratos()
        {
            _logger.LogInformation("Passando pelo ContratoService BuscarTodosContratos.");
                //VAI PARA public interface IContratoRepository CTRL + CLIQUE
                var contratoDetails = await _contratoRepository.BuscarTodasContratos();
            return contratoDetails;
        }

        public async Task<ContratoModel> CriarNovoContrato(CriarContratoDTO dto)
        {
            _logger.LogInformation("Iniciando a criação de um novo Contrato a partir de um DTO.");
            var contrato = _mapper.Map<ContratoModel>(dto);

            if (contrato == null)
            {
                _logger.LogError("Erro de mapeamento: O AutoMapper retornou um objeto nulo para o DTO.");
                throw new InvalidOperationException("Não foi possível mapear o DTO para o modelo de Contrato.");
            }

            // 🔥 NOVO: Calcula status com base na DataFim do DTO antes de salvar
            DefinirStatusInicial(contrato);
            
            await _contratoRepository.CreateNewContrato(contrato);
            await _unitOfWork.CompleteAsync();

            // Registro genérico
            await _auditService.AuditLogAsync(
                entidade: "Contrato",
                acao: "Create",
                payload: dto,
                entidadeId: contrato.IdContrato
            );

            _logger.LogInformation("Contrato criado com sucesso. ID: {IdContrato}", contrato.IdContrato);
            return contrato;
        }


        public async Task<bool> AtualizarAsync(int id, AtualizarContratoDTO dto)
        {
            _logger.LogInformation("Passando pelo Serviço de Atualização de Contrato com ID {Id}", id);
            var contrato = await _unitOfWork.Contrato.BuscarPorIdContrato(id);

            if (contrato == null)
            {
                _logger.LogWarning("Contrato com ID {Id} não encontrado para atualização.", id);
                return false;
            }

            try
            {
                _mapper.Map(dto, contrato);

                //VerificarStatusContrato(id).Wait();

                //_unitOfWork.Contrato.AtualizarContratto(contrato);
                
                var (statusInt, statusDesc) = CalcularStatusContrato(dto.datafim);
                contrato.StatusContrato = statusInt;
                contrato.StatusDescricao = statusDesc;


                await _unitOfWork.CompleteAsync();

                // Registro genérico
                await _auditService.AuditLogAsync(
                    entidade: "Contrato",
                    acao: "Update",
                    payload: dto,
                    entidadeId: contrato.IdContrato
                );

                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Erro de concorrência ao atualizar o Contrato com ID {Id}.", id);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao mapear DTO para ContratoModel.");
                throw;
            }
        }

        private (int statusInt, string statusDesc) CalcularStatusContrato(object dataFim)
        {
            if (dataFim == null)
            {
                return (1, "Contrato ativo");
            }
            DateTime dataFimDateTime;
            if (dataFim is DateTime dt)
            {
                dataFimDateTime = dt;
            }
            else if (DateTime.TryParse(dataFim.ToString(), out DateTime parsedDate))
            {
                dataFimDateTime = parsedDate;
            }
            else
            {
                throw new ArgumentException("DataFim inválida.");
            }
            var hoje = DateTime.UtcNow;
            if (dataFimDateTime < hoje)
            {
                return (0, "Contrato vencido");
            }
            if ((dataFimDateTime - hoje).TotalDays <= 30)
            {
                return (2, "Contrato próximo do vencimento");
            }
            return (1, "Contrato ativo e em dia");
        }

        public async Task<bool> DeletarContrato(int id)
        {
            _logger.LogInformation("Inciando deletação de Contrato com ID, {id}", id);
            var contrato = await _contratoRepository.BuscarPorIdContrato(id);
            if (contrato == null)
                return false;
            _contratoRepository.DeleteContratoAsync(contrato);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<string> VerificarStatusContrato(int idContrato)
        {
            _logger.LogInformation($"Verificando status do contrato ID: {idContrato}");
            var StatusContrato = await _contratoRepository.BuscarPorIdContrato(idContrato);

            if (StatusContrato == null)
            {
                _logger.LogWarning($"Contrato com ID {idContrato} não encontrado.");
                return "Não encontrado";
            }

            if (StatusContrato.DataFim.HasValue && StatusContrato.DataFim.Value < DateTime.UtcNow)
            {
                return "Vencido";
            }

            if (StatusContrato.DataFim.HasValue && (StatusContrato.DataFim.Value - DateTime.UtcNow).TotalDays <= 30)
            {
                return "A vencer";
            }
            return "Ativo";
        }
        private void DefinirStatusInicial(ContratoModel contrato)
        {
            if (!contrato.DataFim.HasValue)
            {
                contrato.StatusContrato = 1;
                contrato.StatusDescricao = "Contrato ativo";
                return;
            }

            var dataFim = contrato.DataFim.Value;
            var hoje = DateTime.UtcNow;

            if (dataFim < hoje)
            {
                contrato.StatusContrato = 0;
                contrato.StatusDescricao = "Contrato vencido";
                return;
            }

            if ((dataFim - hoje).TotalDays <= 30)
            {
                contrato.StatusContrato = 2;
                contrato.StatusDescricao = "Contrato próximo do vencimento";
                return;
            }

            contrato.StatusContrato = 1;
            contrato.StatusDescricao = "Contrato ativo e em dia";
        }

        public async Task<ContratoModel?> BuscarContratoComLicenca(int idContrato)
        {
            _logger.LogInformation("Passando pelo ContratoService BuscarContratoComLicenca.");
            return await _contratoRepository.BuscarContratoComLicenca(idContrato);
        }
        /*private async Task LogCrontratoAsync(CriarContratoDTO dto)
{
   try
   {
       var log = new CriarContratoDTO
       {
           idCliente = dto.idCliente,
           plano = dto.plano,
           qtdLicencas = dto.qtdLicencas,
           dataInicio = dto.dataInicio,
           datafim = dto.datafim,
           periodicidade = dto.periodicidade,
           statusContrato = dto.statusContrato,
           statusDescricao = dto.statusDescricao,
           dataUltimoPagamento = dto.dataUltimoPagamento,
           dataProximoPagamento = dto.dataProximoPagamento,
           observacoes = dto.observacoes
       };
       await _contratoRepository.LogContratoCreationAsync(log);
   }
   catch (Exception ex)
   {
       _logger.LogError(ex, "Erro ao registrar log de criação de contrato.");
   }
}*/
    }
}