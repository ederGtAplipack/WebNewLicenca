using LicencaApi.Data;
using LicencaApi.Interfaces;
using LicencaApi.Models;

namespace LicencaApi.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly LicencaDbContext _context;

        public AuditLogRepository(LicencaDbContext context)
        {
            _context = context;
        }   

        public Task AuditLogAsync(auditlog log)
        {
            _context.auditlog.Add(log);
            return _context.SaveChangesAsync();
        }
    }
}
