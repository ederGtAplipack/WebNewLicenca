using LicencaApi.DTOs;
using LicencaApi.Models;

namespace LicencaApi.Interfaces
{
    public interface IClienteService
    {
        /*VEM DA ANAGRAFICA CONTROLLER, var atualizado = await _clienteService.AtualizarAsync(id, dto);*/
        Task<bool> AtualizarAsync(int id, AtualizarAnagraficaDTO dto);

        /*CHAMA O  public Task<bool> AtualizarAsync(int id, AtualizarAnagraficaDTO dto) NA INTERFACE DO CLIENTESERVICE*/
        Task<AnagraficaModel?> BuscarPorIdCliente(int id);
        Task<IEnumerable<AnagraficaModel>> BuscarTodosClientes();
        Task<AnagraficaModel> CriarNovoCliente(CriarAnagraficaDTO dto);
        Task<bool> DeletarCliente(int id);
    }
}
