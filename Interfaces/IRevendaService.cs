
using LicencaApi.DTOs;
using LicencaApi.Models;

namespace LicencaApi.Interfaces
{
    public interface IRevendaService
    {
        Task<bool> AtualizarAsync(int id, AtualizarRevendaDTO dto);
        Task<RevendaModel?> BuscarPorIdRevenda(int id);
        Task<IEnumerable<RevendaModel>> BuscarTodasRevendas();
        Task<RevendaModel> CriarNewRevenda(CriarRevendaDTO criarRevendaDTO, string idUser);
        Task<bool> DeletarRevenda(int id);
    }
}
