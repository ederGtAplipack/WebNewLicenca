using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicencaApi.Models
{
    [Table("licencaschave")]
    public class LicencasChaveModel
    {
        [Key]
        [Column("idLicencaChave")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLicencaChave { get; set; }

        [Column("Chave")]
        public string? Chave { get; set; }

        [Column("idSoftware")]
        public int? IdSoftware { get; set; }

        [Column("IdRevenda")]
        public int? IdRevenda { get; set; }

        [Column("NumLic")]
        public int? NumLic { get; set; }

        [Column("DataInser")]
        public DateTime? DataInser { get; set; } = DateTime.UtcNow;

        [Column("TipoLic")]
        public string? TipoLic { get; set; }

        [Column("Entregue")]
        public int? Entregue { get; set; }

        [Column("EntreguePara")]
        public string? EntreguePara { get; set; }

        [Column("CreatedAt")]
        public DateTime? CreatedAt { get; set; }

        [Column("Status")]
        public string? Status { get; set; } = "Available";

    }
}
