namespace LicencaApi.DTOs
{
    public class CriarContratoDTO
    {
        //public int idContrato { get; set; }
        public int idCliente { get; set; }
        public string? plano { get; set; } = string.Empty;
        public int qtdLicencas { get; set; }
        public DateTime? dataInicio { get; set; }
        public DateTime? datafim { get; set; }
        public string? periodicidade { get; set; } = string.Empty;
        //public bool pagamentoEmdia { get; set; }
        public string? statusContrato { get; set; } = string.Empty;
        public string? statusDescricao { get; set; } = string.Empty;
        public DateTime dataUltimoPagamento { get; set; }
        public DateTime dataProximoPagamento { get; set; }
        public string? observacoes {  get; set; } = string.Empty;
    }
}
