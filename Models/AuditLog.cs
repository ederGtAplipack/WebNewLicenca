using System.ComponentModel.DataAnnotations.Schema;

namespace LicencaApi.Models
{
    [Table("auditlog")]
    public class auditlog
    {
        public int IdLog { get; set; }
        public string Entidade { get; set; } = string.Empty;
        public int EntidadeId { get; set; }
        public string Acao { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;
        public DateTime DateAuditLog { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string Mesagem { get; set; } = string.Empty;
        
    }
}
