public class LicencaDetalhadaDTO
{
    public int NumLic { get; set; }
    public int IdCliente { get; set; }
    public string? NomeCliente { get; set; }   // Novo: Nome do Cliente vindo de Anagrafica
    public string? TipoLic { get; set; }
    public string? MacAddress { get; set; }
    public DateTime DataLic { get; set; }
    public DateTime Scade { get; set; }
    public bool Attivo { get; set; }
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
    public string? ChaveLicenca { get; set; } // Novo: Chave vindo da tabela LicencasChave
}
