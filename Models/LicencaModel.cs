using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicencaApi.Models
{
    [Table("licenca")]
    public class LicencaModel
    {
        [Key]
        [Column("NumLic")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NumLic { get; set; }
        [Column("IdCliente")]
        public int IdCliente { get; set; }
        public int IdContrato { get; set; }

        [Column("TipoLic")]
        public string? TipoLic { get; set; } = string.Empty;
        [Column("MacAddress")]
        public string? MacAddress { get; set; } = string.Empty;
        [Column("DataLic")]
        public DateTime DataLic { get; set; }
        [Column("Scade")]
        public DateTime Scade { get; set; }
        [Column("Attivo")]
        public int Attivo { get; set; } = 0;
        [Column("IdRevenda")]
        public int IdRevenda { get; set; }
        [Column("SistemaOp")]
        public string? SistemaOp { get; set; } = string.Empty;
        //[Column("dataativacao")]
        public DateTime? DataAtivacao { get; set; }
        [Column("TipoPc")]
        public string? TipoPc { get; set; } = string.Empty;
        [Column("NomeComputador")]
        public string? NomeComputador { get; set; } = string.Empty;
        [Column("Software")]
        public string? Software { get; set; } = string.Empty;
        [Column("ip")]
        public string? ip { get; internal set; }
        [Column("Processador")]
        public string? Processador { get; set; } = string.Empty;
        [Column("Status")]
        public string? Status { get; set; } = "PendingActivation"; // Ex: "Ativa", "Expirada", "Pendente Analise", "Negada"
        [Column("idlicencachave")]
        public int? IdLicencaChave { get;  set; } // Chave de Licença, pode ser um GUID ou outro identificador único
        [Column("MaxDevices")]
        public int MaxDevices { get; set; } = 1; // Número máximo de dispositivos permitidos

        // navigation properties if needed
        [ForeignKey("IdCliente")]
        public AnagraficaModel? IdClienteNavigation { get; internal set; }

        public ContratoModel? Contrato { get; set; }
    }
}
