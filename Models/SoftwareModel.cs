using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicencaApi.Models
{
    [Table("software")]
    public class SoftwareModel
    {
        [Key]
        [Column("idSoftware")]
        public int IdSoftware { get; set; }

        [Column("nSoftware")]
        public string? nSoftware { get; set; } = string.Empty;

        [Column("Descricao")]
        public string? Descricao { get; set; } = string.Empty;
    }
}
