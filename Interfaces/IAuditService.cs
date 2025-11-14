namespace LicencaApi.Interfaces
{
    public interface IAuditService
    {
        Task AuditLogAsync(string entidade, string acao, object payload, int? entidadeId = null);

    }
}