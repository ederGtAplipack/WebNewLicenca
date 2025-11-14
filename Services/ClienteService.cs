using AutoMapper;
using LicencaApi.Data;
using LicencaApi.DTOs;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics.Contracts;

namespace LicencaApi.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ILogger<ClienteService> _logger;
        private readonly IClienteRepository _clienteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;

        public ClienteService(IClienteRepository repository, IUnitOfWork unitOfWork, ILogger<ClienteService> logger, IMapper mapper, IAuditService auditService)
        {
            _clienteRepository = repository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _auditService = auditService;
        }

        public async Task<AnagraficaModel?> BuscarPorIdCliente(int id)
        {
            _logger.LogInformation("Passando pelo ClienteService.BuscarPorIdCliente.");
            return await _clienteRepository.BuscarPorIdCliente(id);
        }

        public async Task<IEnumerable<AnagraficaModel>> BuscarTodosClientes()
        {
            //_logger.LogInformation("Passando pelo ClienteService.BuscarTodosClientes.");
            return await _clienteRepository.BuscarTodasAnagrafica();
        }

        public async Task<AnagraficaModel> CriarNovoCliente(CriarAnagraficaDTO dto)
        {
            _logger.LogInformation("Iniciando a criação de um novo cliente a partir de um DTO.");
            var anagrafica = _mapper.Map<AnagraficaModel>(dto);

            if (anagrafica == null)
            {
                _logger.LogError("Erro de mapeamento: O AutoMapper retornou um objeto nulo para o DTO.");
                throw new InvalidOperationException("Não foi possível mapear o DTO para o modelo Anagrafica.");
            }

            await _clienteRepository.CreateNewAnagrafica(anagrafica);
            await _unitOfWork.CompleteAsync();

            // Registro genérico
            await _auditService.AuditLogAsync(
                entidade: "Cliente",
                acao: "Create",
                payload: dto,
                entidadeId: anagrafica.IdAnagrafica
            );

            _logger.LogInformation("Cliente criado com sucesso. ID: {IdAnagrafica}", anagrafica.IdAnagrafica);
            return anagrafica;
        }

        public async Task<bool> AtualizarAsync(int id, AtualizarAnagraficaDTO dto)
        {
            _logger.LogInformation("Passando pelo Serviço de Atualização de Cliente com ID {Id}", id);
            var cliente = await _unitOfWork.Cliente.BuscarPorIdCliente(id);

            if (cliente == null)
            {
                _logger.LogWarning("Cliente com ID {Id} não encontrado para atualização.", id);
                return false;
            }

            try
            {
                _mapper.Map(dto, cliente);
                _unitOfWork.Cliente.AtualizarAnagrafica(cliente);
                await _unitOfWork.CompleteAsync();

                // Registro genérico
                await _auditService.AuditLogAsync(
                    entidade: "Cliente",
                    acao: "Update",
                    payload: dto,
                    entidadeId: cliente.IdAnagrafica
                );

                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Erro de concorrência ao atualizar o cliente com ID {Id}.", id);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao mapear DTO para AnagraficaModel.");
                throw;
            }
        }

        public async Task<bool> DeletarCliente(int id)
        {
            _logger.LogInformation("Inciando deletação de Cliente com ID, {id}", id);
            var anagrafica = await _clienteRepository.BuscarPorIdCliente(id);
            if (anagrafica == null)
                return false;
            _clienteRepository.DeleteAnagraficaAsync(anagrafica);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}