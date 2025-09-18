using LicencaApi.DTOs;
using LicencaApi.Models;

namespace LicencaApi.Interfaces
{
    public interface IContratoService
    {
        Task<bool> AtualizarAsync(int id, AtualizarContratoDTO dto);
        Task<ContratoModel?> BuscarPorIdContrato(int id);
        //VAI PARA public async Task<IEnumerable<ContratoModel>> BuscarTodosContratos() CTRL + F12
        //EM public class ContratoService : IContratoService
        Task<IEnumerable<ContratoModel>> BuscarTodosContratos();
        Task<ContratoModel> CriarNovoContrato(CriarContratoDTO dto);
        Task<bool> DeletarContrato(int id);
    }
}