namespace LicencaApi.DTOs
{
    public class AtualizarContratoDTO
    {
        //public int idContrato { get; set; }
        public int idCliente { get; set; }
        public string? plano { get; set; } = string.Empty;
        public int qtdlicencas { get; set; }
        public DateTime? dataInicio { get; set; }
        public DateTime? datafim { get; set; }
        public string? periodicidade { get; set; } = string.Empty;
        public int pagamentoEmdia { get; set; }
        public string? statusContrato { get; set; } = string.Empty;
        public string? statusDescricao { get; set; } = string.Empty;
        public DateTime dataUltimoPagamento { get; set; }
        public DateTime dataProximoPagamento { get; set; }
        public string? observacoes { get; set; } = string.Empty;

    }
}
