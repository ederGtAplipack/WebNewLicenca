using LicencaApi.Interfaces;
using LicencaApi.Models;
using System.Text.Json;

namespace LicencaApi.Services
{
    public class AuditService : IAuditService
    {
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly ILogger<AuditService> _logger;

        public AuditService(IAuditLogRepository auditLogRepository, ILogger<AuditService> logger)
        {            
            _logger = logger;
            _auditLogRepository = auditLogRepository;
        }

        // Implement audit-related methods here
        public async Task AuditLogAsync(string entidade, string acao, object payload, int? entidadeId = null)
        {
            try
            {
                var log = new auditlog
                {
                    Entidade = entidade,
                    Acao = acao,
                    EntidadeId = entidadeId ?? 0,
                    Payload = JsonSerializer.Serialize(payload),
                    DateAuditLog = DateTime.UtcNow,
                    Usuario = "Sistema" // ou pegar de Claims JWT se houver login
                };

                await _auditLogRepository.AuditLogAsync(log);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar log genérico.");
            }
        }
    }
}
