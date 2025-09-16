using LicencaApi.Models;

namespace LicencaApi.Interfaces
{
    public interface IContratoRepository
    {
        void AtualizarContratto(ContratoModel contratoModel);
        Task<ContratoModel?> BuscarPorIdContrato(int id);
        Task<IEnumerable<ContratoModel>> BuscarTodasContratos();
        Task CreateNewContrato(ContratoModel contratoModel);
        Task DeleteContratoAsync(ContratoModel contrato);
    }
}