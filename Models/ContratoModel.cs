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
        public string Periodicidade { get; set; } = "Mensal"; // ou Anual, Trimestral, Vitalício
        public bool PagamentoEmDia { get; set; } = true; // novo campo
        public string StatusContrato { get; set; } = "Ativo"; // Ex: Ativo, Inativo, Suspenso
        public string StatusDescricao { get; set; } = "Contrato ativo e em dia"; // Descrição do status do contrato
        public DateTime? DataUltimoPagamento { get; set; } // Data do último pagamento, se aplicável
        public DateTime? DataProximoPagamento { get; set; } // Data do próximo pagamento, se aplicável
        public string? Observacoes { get; set; } // Campo opcional para observações adicionais

    }
}
