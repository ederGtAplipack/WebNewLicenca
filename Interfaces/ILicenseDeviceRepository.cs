using LicencaApi.DTOs;

namespace LicencaApi.Interfaces
{
    public interface ILicenseDeviceRepository
    {
        Task<IEnumerable<LicencaDeviceDTO>> GetAllDevicesAsync();
    }
}
