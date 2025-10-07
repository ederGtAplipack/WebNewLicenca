using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicencaApi.Models
{
    [Table("contrato")]
    public class ContratoModel
    {
        [Key]
        [Column("idContrato")]
        public int IdContrato { get; set; }
        [Column("idCliente")]
        public int IdCliente { get; set; }

        [Column("plano")]
        public string? Plano { get; set; }

        [Column("qtdLicencas")]
        public int QtdLicencas { get; set; }

        [Column("dataInicio")]
        public DateTime DataInicio { get; set; }

        [Column("dataFim")]
        public DateTime? DataFim { get; set; }
        public string Periodicidade { get; set; } = "Mensal"; // ou Anual, Trimestral, Vitalício
        public int PagamentoEmDia { get; set; } = 1; // novo campo
        public string StatusContrato { get; set; } = "Ativo"; // Ex: Ativo, Inativo, Suspenso
        public string StatusDescricao { get; set; } = "Contrato ativo e em dia"; // Descrição do status do contrato
        public DateTime? DataUltimoPagamento { get; set; } // Data do último pagamento, se aplicável
        public DateTime? DataProximoPagamento { get; set; } // Data do próximo pagamento, se aplicável
        public string? Observacoes { get; set; } // Campo opcional para observações adicionais

    }
}
