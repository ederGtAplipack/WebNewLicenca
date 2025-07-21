using LicencaApi.Models;

namespace LicencaApi.Repositories
{
    public interface ILicencaRepository
    {
        Task<IEnumerable<LicencaModel>> BuscarTodasAsync();
        Task<LicencaModel?> BuscarPorIdAsync(int id);
        Task<IEnumerable<LicencaModel>> BuscarAtivasAsync();
        Task<LicencaModel> CriarAsync(LicencaModel model);
        Task<bool> AtualizarAsync(LicencaModel model);
        Task<bool> DesativarAsync(int id);
    }
}
