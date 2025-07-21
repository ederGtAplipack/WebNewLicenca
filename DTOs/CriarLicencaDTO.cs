namespace LicencaApi.DTOs
{
    public class CriarLicencaDTO
    {
        // Propriedades que representam os dados necessários para criar uma nova licença
        public int NumLic { get; set; }
        public int IdCliente { get; set; }
        public string? TipoLic { get; set; } = string.Empty;
        public string? MacAddress { get; set; } = string.Empty;
        public DateTime DataLic { get; set; }
        public DateTime Scade { get; set; }
        public bool Attivo { get; set; }
        public int IdRevenda { get; set; }
        public string? SistemaOp { get; set; } = string.Empty;
        public DateTime DataAtivacao { get; set; }
        public string? TipoPc { get; set; } = string.Empty;
        public string? NomeComputador { get; set; } = string.Empty;
        public string? Software { get; set; } = string.Empty;
        public string? Ip { get; set; } = string.Empty;
        public string? Processador { get; set; } = string.Empty;
        public string? IdLicencaChave { get; set; } = string.Empty;

    }
}
