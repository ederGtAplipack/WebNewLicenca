using AutoMapper;
using Humanizer;
using LicencaApi.Data;
using LicencaApi.DTOs;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using LicencaApi.Repositories;
using Microsoft.AspNetCore.Mvc;

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
    }
}
