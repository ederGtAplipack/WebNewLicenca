using AutoMapper;
using LicencaApi.Data;
using LicencaApi.DTOs;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LicencaApi.Services
{
    public class ContratoService : IContratoService
    {
        private readonly ILogger<ContratoService> _logger;
        private readonly IContratoRepository _contratoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContratoService(IUnitOfWork unitOfWork, ILogger<ContratoService> logger, IMapper mapper, IContratoRepository repository)
        {
            _contratoRepository = repository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
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

            await _contratoRepository.CreateNewContrato(contrato);
            await _unitOfWork.CompleteAsync();

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
                _unitOfWork.Contrato.AtualizarContratto(contrato);
                await _unitOfWork.CompleteAsync();
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
    }
}