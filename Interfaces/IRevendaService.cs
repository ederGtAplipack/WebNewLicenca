
using LicencaApi.DTOs;
using LicencaApi.Models;

namespace LicencaApi.Interfaces
{
    public interface IRevendaService
    {
        Task<IEnumerable<RevendaModel>> BuscarTodasRevendas();
        Task<RevendaModel> CriarNewRevenda(CriarRevendaDTO criarRevendaDTO, string idUser);
    }
}
