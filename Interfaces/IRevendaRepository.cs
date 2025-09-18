using LicencaApi.Models;

namespace LicencaApi.Interfaces
{
    public interface IRevendaRepository
    {
        Task<IEnumerable<RevendaModel>> BuscarTodasRevendas();
        Task CreateNewRevenda(RevendaModel revenda);
        Task AssociateRevendaUser(RevendaUserModel revendaUser);
    }
}
