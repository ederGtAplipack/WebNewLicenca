using LicencaApi.DTOs;
using LicencaApi.Models;

namespace LicencaApi.Interfaces
{
    public interface ILicencaRepository
    {
        Task<IEnumerable<LicencaModel>> BuscarTodasAsync();
        //Task<List<LicencaModel>> BuscarComPaginacaoAsync(int page, int pageSize);
        Task<LicencaModel?> BuscarPorIdAsync(int id);
		Task CriarAsync(LicencaModel model);
        Task AtualizarAsync(LicencaModel model);
        Task SalvarAsync();		
        
    }
}
