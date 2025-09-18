
using LicencaApi.Models;

namespace LicencaApi.Interfaces
{
    public interface IRevendaService
    {
        Task<IEnumerable<RevendaModel>> BuscarTodasRevendas();
    }
}
