using LicencaApi.DTOs;
using LicencaApi.Models;

namespace LicencaApi.Interfaces
{
    public interface ISoftwareService
    {
        Task<bool> AtualizarAsync(int id, AtualizarSoftwareDTO dto);
        Task <SoftwareModel> CreateNewSoftware(CriarSoftwareDTO dto);
        Task<bool> DeletarAsync(int id);
        Task<IEnumerable<SoftwareModel>> GetAllSoftwaresAsync();
        Task<SoftwareModel?> GetByIdAsync(int id);
    }
}