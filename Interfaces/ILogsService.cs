using LicencaApi.Models;
using System.Collections;

namespace LicencaApi.Interfaces
{
    public interface ILogsService
    {
        Task<IEnumerable<LicencaLogModel>> GetLogs();
    }
}