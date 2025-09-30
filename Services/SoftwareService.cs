using AutoMapper;
using LicencaApi.Data;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LicencaApi.Services
{
    public class SoftwareService : ISoftwareService
    {
        // Implementation of the SoftwareService class
        private readonly ILogger<SoftwareService> _logger;
        private readonly ISoftwareRepository _softwareRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SoftwareService(ILogger<SoftwareService> logger, ISoftwareRepository softwareRepository, IUnitOfWork unitOfWork, IMapper mapper )
        {
            _logger = logger;
            _softwareRepository = softwareRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SoftwareModel>> GetAllSoftwaresAsync()
        {
            _logger.LogInformation("Passando pelo Service do GetAllSoftwaresAsync.");
            return await _softwareRepository.GetAllSoftwaresAsync();

        }
    }
}
