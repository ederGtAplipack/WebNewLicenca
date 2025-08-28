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
        void AtualizarAsync(LicencaModel model);
        Task SalvarAsync();	
        Task<IEnumerable<LicencaModel>> BuscarAtivasAsync();
        Task<bool> DesativarAsync(int id);
        Task<IEnumerable<LicencaDetalhadaDTO>> ObterTodasComDetalhesAsync();
    }
}
