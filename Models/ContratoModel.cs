using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicencaApi.Models
{
    [Table("contrato")]
    public class ContratoModel
    {
        [Key]
        [Column("id_contrato")]
        public string IdContrato { get; set; } = string.Empty;
        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [Column("plano")]
        public string? Plano { get; set; }

        [Column("qtd_licencas")]
        public int QtdLicencas { get; set; }

        [Column("data_inicio")]
        public DateTime DataInicio { get; set; }

        [Column("data_fim")]
        public DateTime? DataFim { get; set; }
    }
}
