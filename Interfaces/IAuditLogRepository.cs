using LicencaApi.Models;

namespace LicencaApi.Interfaces
{
    public interface IAuditLogRepository
    {
        Task AuditLogAsync(auditlog log);
    }
}