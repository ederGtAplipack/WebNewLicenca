using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicencaApi.Models
{
    [Table("licencaschave")]
    public class LicencasChaveModel
    {
        [Key]
        [Column("idLicencaChave")]
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
        public DateTime? DataInser { get; set; }

        [Column("TipoLic")]
        public string? TipoLic { get; set; }

        [Column("Entregue")]
        public int? Entregue { get; set; }

        [Column("EntreguePara")]
        public string? EntreguePara { get; set; }

        // Navegação
        /*[ForeignKey("idSoftware")]
        public virtual SoftwareModel? Software { get; set; }*/
    }
}
