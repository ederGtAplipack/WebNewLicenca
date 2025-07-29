using System.ComponentModel.DataAnnotations;

namespace LicencaApi.DTOs
{
    public class ContratoStatusUpdateDTO
    {

        /// Indica se o pagamento do contrato está em dia.
        [Required(ErrorMessage = "O campo PagamentoEmDia é obrigatório.")]
        public bool PagamentoEmDia { get; set; }

        /// Status do contrato: "Ativo", "Suspenso", "Cancelado", etc.
        [Required(ErrorMessage = "O campo StatusContrato é obrigatório.")]
        [StringLength(50, ErrorMessage = "O StatusContrato deve ter no máximo 50 caracteres.")]
        public string StatusContrato { get; set; } = "Ativo";
    }
}
