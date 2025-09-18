using AutoMapper;
using LicencaApi.Data;
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
            return await _revendaRepository.BuscarTodasAnagrafica();
        }

        

    }
}
