using LicencaApi.Models;

namespace LicencaApi.Interfaces
{
    public interface IContratoRepository
    {
        void AtualizarContratto(ContratoModel contratoModel);
        Task<ContratoModel?> BuscarPorIdContrato(int id);
        //VAI PARA public class ContratoRepository : IContratoRepository CTRL + F12 QUE É O "FIM"
        Task<IEnumerable<ContratoModel>> BuscarTodasContratos();
        Task CreateNewContrato(ContratoModel contratoModel);
        Task DeleteContratoAsync(ContratoModel contrato);
    }
}