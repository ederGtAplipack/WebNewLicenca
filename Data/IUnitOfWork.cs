using LicencaApi.Interfaces;
using LicencaApi.Repositories;
using System;
using System.Threading.Tasks;

namespace LicencaApi.Data
{
    //herança da interface IDisposable, garantindo que recursos possam ser liberados corretamente.
    public interface IUnitOfWork : IDisposable
    {
        //Uma propriedade chamada Licencas, que expõe um repositório específico (ILicencaRepository) relacionado a licenças.
        ILicencaRepository Licencas { get; }

        IClienteRepository Cliente { get; }

        IContratoRepository Contrato { get; }

        IRevendaRepository Revenda { get; }
        Task<int> CompleteAsync();
     }
}