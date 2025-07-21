using LicencaApi.Repositories;

namespace LicencaApi.Data
{
    public interface IUnitOfWork : IDisposable
    {
        ILicencaRepository Licencas { get; }    
        Task<int> CompleteAsync();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LicencaDbContext _context;
        private bool _disposed;
        public UnitOfWork(LicencaDbContext context)
        {
            _context = context;
            Licencas = new LicencaRepository(_context);
        }
        public ILicencaRepository Licencas { get; }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
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
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
