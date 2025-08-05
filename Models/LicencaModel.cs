using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicencaApi.Models
{
    [Table("licenca")]
    public class LicencaModel
    {
        [Key]
        [Column("NumLic")]
        public int NumLic { get; set; }
        [Column("IdCliente")]
        public int IdCliente { get; set; }
        [Column("TipoLic")]
        public string? TipoLic { get; set; } = string.Empty;
        [Column("MacAddress")]
        public string? MacAddress { get; set; } = string.Empty;
        [Column("DataLic")]
        public DateTime DataLic { get; set; }
        [Column("Scade")]
        public DateTime Scade { get; set; }
        [Column("Attivo")]
        public bool Attivo { get; set; }
        [Column("IdRevenda")]
        public int IdRevenda { get; set; }
        [Column("SistemaOp")]
        public string? SistemaOp { get; set; } = string.Empty;
        //[Column("dataativacao")]
        public DateTime? DataAtivacao { get; set; }
        [Column("Tipo_Pc")]
        public string? Tipo_Pc { get; set; } = string.Empty;
        [Column("Nome_Computador")]
        public string? Nome_Computador { get; set; } = string.Empty;
        [Column("Software")]
        public string? Software { get; set; } = string.Empty;
        [Column("ip")]
        public string? Ip { get; set; } = string.Empty;
        [Column("Processador")]
        public string? Processador { get; set; } = string.Empty;
        [Column("Status")]
        public string? Status { get; set; } = string.Empty; // Ex: "Ativa", "Expirada", "Pendente Analise", "Negada"
        [Column("idlicencachave")]
        public int? IdLicencaChave { get;  set; } // Chave de Licença, pode ser um GUID ou outro identificador único
       
	   // FK
        /*[ForeignKey("IdCliente")]
        public virtual AnagraficaModel? Anagrafica { get; set; }
		[ForeignKey("idlicencachave")]
        public virtual LicencasChaveModel? LicencasChave { get; set; }
		[ForeignKey("IdRevenda")]
        public virtual RevendaModel? Revenda { get; set; }    */
	   
    }
}
