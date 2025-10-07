using AutoMapper;
using LicencaApi.Data;
using LicencaApi.DTOs;
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

        public async Task<SoftwareModel?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Passando pelo Service do GetByIdAsync.");
            return await _softwareRepository.GetByIdAsync(id);
        }
        public async Task<SoftwareModel> CreateNewSoftware(CriarSoftwareDTO dto)
        {
            _logger.LogInformation("Iniciando a criação de um novo software a partir de um DTO.");
            var software = _mapper.Map<SoftwareModel>(dto);
            await _softwareRepository.CreateNewSoftware(software);
            await _unitOfWork.CompleteAsync(); // Salva o software para obter o ID
            _logger.LogInformation("Software criado com sucesso. ID: {IdSoftware}", software.IdSoftware);
            return software;
        }

        public async Task<bool> AtualizarAsync(int id, AtualizarSoftwareDTO dto)
        {
            var existingSoftware = await _softwareRepository.GetByIdAsync(id);
            if (existingSoftware == null)
            {
                _logger.LogWarning("Software com ID {Id} não encontrado para atualização.", id);
                return false;
            }
            // Atualiza os campos do software existente com os valores do DTO
            _mapper.Map(dto, existingSoftware);
            _softwareRepository.Update(existingSoftware);
            await _unitOfWork.CompleteAsync();
            _logger.LogInformation("Software com ID {Id} atualizado com sucesso.", id);
            return true;
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var existingSoftware = await _softwareRepository.GetByIdAsync(id);
            if (existingSoftware == null)
            {
                _logger.LogWarning("Software com ID {Id} não encontrado para exclusão.", id);
                return false;
            }
            _softwareRepository.Delete(existingSoftware);
            await _unitOfWork.CompleteAsync();
            _logger.LogInformation("Software com ID {Id} excluído com sucesso.", id);
            return true;
        }
    }
}
