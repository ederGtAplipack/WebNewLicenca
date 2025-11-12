using LicencaApi.Models;

namespace LicencaApi.Interfaces
{
    public interface ILogsRepository
    {
        Task<IEnumerable<LicencaLogModel>> GetAllLogsAsync();
    }
}