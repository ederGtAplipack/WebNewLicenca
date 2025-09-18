using LicencaApi.Models;

namespace LicencaApi.Interfaces
{
    public interface IRevendaRepository
    {
        Task<IEnumerable<RevendaModel>> BuscarTodasAnagrafica();
    }
}
