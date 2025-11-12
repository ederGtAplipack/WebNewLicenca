
using LicencaApi.DTOs;

namespace LicencaApi.Interfaces
{
    public interface ILicenseDeviceService
    {
        Task<IEnumerable<LicencaDeviceDTO>> GetLicenseDeviceStatusAsync();
    }
}
