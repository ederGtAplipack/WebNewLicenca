using LicencaApi.Models;

namespace LicencaApi.Interfaces
{
    public interface ISoftwareRepository
    {
        Task<IEnumerable<SoftwareModel>> GetAllSoftwaresAsync();
    }
}