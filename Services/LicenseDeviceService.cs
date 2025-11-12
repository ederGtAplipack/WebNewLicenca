using LicencaApi.Data;
using LicencaApi.DTOs;
using LicencaApi.Interfaces;
using System.Runtime.CompilerServices;

namespace LicencaApi.Services
{
    public class LicenseDeviceService : ILicenseDeviceService
    {
        private readonly ILicenseDeviceRepository _deviceRepository;
        private readonly ILogger<LicenseDeviceService> _logger;
        private readonly LicencaDbContext _context;

        public LicenseDeviceService(ILicenseDeviceRepository deviceRepository, ILogger<LicenseDeviceService> logger, LicencaDbContext context)
        {
            _logger = logger;
            _context = context;
            _deviceRepository = deviceRepository;
        }
        public async Task<IEnumerable<LicencaDeviceDTO>> GetLicenseDeviceStatusAsync()
        {
            _logger.LogInformation("Obtendo status do dispositivo de licença.");
            var devices = await _deviceRepository.GetAllDevicesAsync();
            return devices;
        }
    }
}
