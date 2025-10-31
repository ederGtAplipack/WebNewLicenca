namespace LicencaApi.DTOs
{
    public class AtualizarLicencaDTO
    {
        //public int NumLic { get; set; }
        //public int IdCliente { get; set; }
        public string TipoLic { get; set; } = string.Empty;
        public string MacAddress { get; set; } = string.Empty;  
        public DateTime DataLic { get; set; }
        public DateTime Scade { get; set; }
        public int MaxDevices { get; set; }
        public int Attivo { get; set; }
        public int IdRevenda { get; set; }
        public int IdSoftware { get; set; }
        public string SistemaOp { get; set; } = string.Empty;
        public DateTime DataAtivacao { get; set; }
        public string TipoPc { get; set; } = string.Empty;
        public string NomeComputador { get; set; } = string.Empty;
        public string Software { get; set; } = string.Empty;
        public string Ip { get; set; } = string.Empty;
        public string Processador { get; set; } = string.Empty;
        public int? IdLicencaChave { get; set; }
        public string? Status { get; set; } = string.Empty;
    }
}
