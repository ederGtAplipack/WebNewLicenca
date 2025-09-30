using LicencaApi.Models;

namespace LicencaApi.Interfaces
{
    public interface ISoftwareRepository
    {
        Task CreateNewSoftware(SoftwareModel software);
        void Delete(SoftwareModel existingSoftware);
        Task<IEnumerable<SoftwareModel>> GetAllSoftwaresAsync();
        Task<SoftwareModel?> GetByIdAsync(int id);
        void Update(SoftwareModel existingSoftware);
    }
}