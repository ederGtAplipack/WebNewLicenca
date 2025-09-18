using LicencaApi.DTOs;
using LicencaApi.Models;

namespace LicencaApi.Interfaces
{
    public interface ILicencaRepository
    {
        Task<IEnumerable<LicencaModel>> BuscarTodasAsync();
        Task<LicencaModel?> BuscarPorIdAsync(int id);
        //Task<AnagraficaModel?> BuscarPorIdAnagrafica(int id);

		Task CriarAsync(LicencaModel model);   
        Task CreateSql(LicencaModel licencaDetalhadaDTO);
        //Task CreateNewAnagrafica(AnagraficaModel anagraficaModel);

        void AtualizarAsync(LicencaModel model);
        Task SalvarAsync();	
        Task<IEnumerable<LicencaModel>> BuscarAtivasAsync();
        Task<bool> DesativarAsync(int id);
        Task<IEnumerable<LicencaDetalhadaDTO>> ObterTodasComDetalhesAsync();
        /* O método CreateSql é responsável por criar uma nova licença no banco de dados
         * a partir dos dados fornecidos no modelo LicencaModel.
         * Ele retorna uma tarefa assíncrona que representa a operação de criação.
         */
        //Task<IEnumerable<AnagraficaModel>> BuscarTodasAnagrafica();
    }
}
