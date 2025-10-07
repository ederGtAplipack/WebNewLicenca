// LicencaApi/Data/UnitOfWork.cs

using LicencaApi.Interfaces;
using LicencaApi.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LicencaApi.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LicencaDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;
        private bool _disposed;

        // A propriedade agora é pública, somente leitura e armazena o repositório.
        public ILicencaRepository Licencas { get; }
        public IClienteRepository Cliente {  get; }
        public IContratoRepository Contrato { get; }
        public IRevendaRepository Revenda { get; }
        public ISoftwareRepository Software { get; }


        // O construtor recebe todas as dependências por injeção.
        public UnitOfWork(LicencaDbContext context, ILogger<UnitOfWork> logger,
            ILicencaRepository licencas,
            IClienteRepository cliente,
            IContratoRepository contrato,
            IRevendaRepository revenda,
            ISoftwareRepository software)
        {
            _context = context;
            _logger = logger;
            // Atribua o repositório injetado à propriedade.
            this.Licencas = licencas;
            this.Cliente = cliente;
            this.Contrato = contrato;
            Revenda = revenda;
            Software = software;
        }

        public async Task<int> CompleteAsync()
        {
            _logger.LogInformation("Iniciando a transação de salvamento no UnitOfWork.");
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}