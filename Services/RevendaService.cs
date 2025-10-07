using AutoMapper;
using Humanizer;
using LicencaApi.Data;
using LicencaApi.DTOs;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using LicencaApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LicencaApi.Services
{
    public class RevendaService : IRevendaService
    {
        private readonly ILogger<RevendaService> _logger;
        private readonly IRevendaRepository _revendaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RevendaService(ILogger<RevendaService> logger, IRevendaRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _revendaRepository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<RevendaModel?> BuscarPorIdRevenda(int id)
        {
            _logger.LogInformation("Passando pelo ClienteService.BuscarPorIdCliente.");
            return await _revendaRepository.BuscarPorIdRevenda(id);
        }

        public async Task<IEnumerable<RevendaModel>> BuscarTodasRevendas()
        {
            _logger.LogInformation("Passando pelo RevendasService.BuscarTodasRevendas.");
            return await _revendaRepository.BuscarTodasRevendas();
        }

        public async Task<RevendaModel> CriarNewRevenda(CriarRevendaDTO criarRevendaDTO, string idUser)
        {
            _logger.LogInformation("Iniciando a criação de uma nova revenda a partir de um DTO.");
            var revenda = _mapper.Map<RevendaModel>(criarRevendaDTO);

            await _revendaRepository.CreateNewRevenda(revenda);
            await _unitOfWork.CompleteAsync(); // Salva a revenda para obter o ID

            // Associa o usuário logado à revenda criada
            var revendaUser = new RevendaUserModel
            {
                idRevenda = revenda.idRevenda,
                idUser = idUser
            };

            await _revendaRepository.AssociateRevendaUser(revendaUser);
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation("Revenda criada e associada ao usuário com sucesso. ID: {IdRevenda}", revenda.idRevenda);
            return revenda;
        }

        public async Task<bool> AtualizarAsync(int id, AtualizarRevendaDTO dto)
        {
            _logger.LogInformation("Passando pelo Serviço de Atualização de Revenda com ID {Id}", id);
            var revenda = await _unitOfWork.Revenda.BuscarPorIdRevenda(id);

            if (revenda == null)
            {
                _logger.LogWarning("Revenda com ID {Id} não encontrado para atualização.", id);
                return false;
            }

            try
            {
                _mapper.Map(dto, revenda);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Erro de concorrência ao atualizar o Revenda com ID {Id}.", id);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao mapear DTO para Revendamodel.");
                throw;
            }

        }

        public async Task<bool> DeletarRevenda(int id)
        {
            _logger.LogInformation("Inciando deletação de Revenda com ID, {id}", id);
            var revenda = await _revendaRepository.BuscarPorIdRevenda(id);
            if (revenda == null)
                return false;
            _revendaRepository.DeleteRevendaAsync(revenda);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<AnagraficaModel>> ListarClientesPorRevendaAsync(int idRevenda)
        {
            _logger.LogInformation("Serviço de Revenda: buscando clientes por ID da revenda.");
            return await _revendaRepository.ListarClientesPorRevendaAsync(idRevenda);
        }
    }
}
