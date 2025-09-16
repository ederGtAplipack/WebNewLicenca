using LicencaApi.Models;

namespace LicencaApi.Interfaces
{
    public interface IClienteRepository
    {
        Task<AnagraficaModel?> BuscarPorIdCliente(int id);
        Task<IEnumerable<AnagraficaModel>> BuscarTodasAnagrafica();
        Task CreateNewAnagrafica(AnagraficaModel anagraficaModel);
        void AtualizarAnagrafica(AnagraficaModel model);
        Task DeleteAnagraficaAsync(AnagraficaModel anagrafica);
    }
}