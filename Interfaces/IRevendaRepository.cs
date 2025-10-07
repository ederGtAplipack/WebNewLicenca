using LicencaApi.Models;

namespace LicencaApi.Interfaces
{
    public interface IRevendaRepository
    {
        Task<IEnumerable<RevendaModel>> BuscarTodasRevendas();
        Task CreateNewRevenda(RevendaModel revenda);
        Task AssociateRevendaUser(RevendaUserModel revendaUser);
        Task<RevendaModel?> BuscarPorIdRevenda(int id);
        void DeleteRevendaAsync(RevendaModel revenda);
        Task<IEnumerable<AnagraficaModel>> ListarClientesPorRevendaAsync(int idRevenda);
    }
}
