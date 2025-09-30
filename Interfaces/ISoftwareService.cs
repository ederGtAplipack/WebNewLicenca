using LicencaApi.Models;

namespace LicencaApi.Interfaces
{
    public interface ISoftwareService
    {
        // Define the methods that the SoftwareService class must implement

        Task<IEnumerable<SoftwareModel>> GetAllSoftwaresAsync();
    }
}