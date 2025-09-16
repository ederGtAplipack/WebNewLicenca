using LicencaApi.DTOs;
using LicencaApi.Models;

namespace LicencaApi.Interfaces
{
    public interface IContratoService
    {
        Task<bool> AtualizarAsync(int id, AtualizarContratoDTO dto);
        Task<ContratoModel?> BuscarPorIdContrato(int id);
        Task<IEnumerable<ContratoModel>> BuscarTodosContratos();
        Task<ContratoModel> CriarNovoContrato(CriarContratoDTO dto);
        Task<bool> DeletarContrato(int id);
    }
}