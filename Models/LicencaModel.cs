using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicencaApi.Models
{
    [Table("licenca")]
    public class LicencaModel
    {
        [Key]
        [Column("numLic")]
        public int NumLic { get; set; }
        //[Column("id_cliente")]
        public int IdCliente { get; set; }
        //[Column("tipo_lic")]
        public string? TipoLic { get; set; } = string.Empty;
        //[Column("mac_address")]
        public string? MacAddress { get; set; } = string.Empty;
        //[Column("data_lic")]
        public DateTime DataLic { get; set; }
        [Column("scade")]
        public DateTime Scade { get; set; }
        [Column("attivo")]
        public bool Attivo { get; set; }
        //[Column("id_revenda")]
        public int IdRevenda { get; set; }
        //[Column("sistema_op")]
        public string? SistemaOp { get; set; } = string.Empty;
        //[Column("data_ativacao")]
        public DateTime? DataAtivacao { get; set; }
        [Column("tipo_pc")]
        public string? Tipo_Pc { get; set; } = string.Empty;
        [Column("nome_computador")]
        public string? Nome_Computador { get; set; } = string.Empty;
        [Column("software")]
        public string? Software { get; set; } = string.Empty;
        [Column("ip")]
        public string? Ip { get; set; } = string.Empty;
        [Column("processador")]
        public string? Processador { get; set; } = string.Empty;
        //[Column("id_licenca_chave")]
        public string? Status { get; set; } = string.Empty; // Ex: "Ativa", "Expirada", "Pendente Analise", "Negada"
        public string IdLicencaChave { get; internal set; }
    }
}
