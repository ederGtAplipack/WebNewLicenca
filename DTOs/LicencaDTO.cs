namespace LicencaApi.DTOs
{
    public class LicencaDTO
    {
        public int NumLic { get; set; }
        public string Chave { get; set; }
        public int IdCliente { get; set; }
        public int IdContrato { get; set; }
        public string? TipoLic { get; set; }
        public string? MacAddress { get; set; }
        public DateTime DataLic { get; set; }
        public DateTime Scade { get; set; }
        public int Attivo { get; set; }
        public int IdRevenda { get; set; }
        public string? SistemaOp { get; set; }
        public DateTime? DataAtivacao { get; set; }
        public string? TipoPc { get; set; }
        public string? NomeComputador { get; set; }
        public string? Software { get; set; }
        public string? Ip { get; set; }
        public string? Processador { get; set; }
        public string? Status { get; set; }
        public int? IdLicencaChave { get; set; }
        public string Mensagem { get; set; }        
    }
}
