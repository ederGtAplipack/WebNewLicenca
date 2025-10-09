public class LicencaDetalhadaDTO
{
    // LICENÇA
    public int NumLic { get; set; }
    public int? IdCliente { get; set; }
    public string? TipoLic { get; set; }
    public string? StatusLicenca { get; set; }
    public DateTime DataLic { get; set; }
    public DateTime Scade { get; set; }
    public DateTime? DataAtivacao { get; set; }

    // CLIENTE
    public string? NomeCliente { get; set; }
    public string? CNPJCliente { get; set; }
    public string? EmailCliente { get; set; }

    // CONTRATO
    public int? IdContrato { get; set; }
    public string? StatusContrato { get; set; }
    public string? PlanoContrato { get; set; }
    public int? QtdLicencasContrato { get; set; }

    // CHAVE
    public int? IdLicencaChave { get; set; }
    public string? ChaveLicenca { get; set; }
    public string? StatusChave { get; set; }

    // REVENDA
    public int? IdRevenda_Revenda { get; set; }
    public string? RazaoSocialRevenda { get; set; }

    // SOFTWARE
    public int? IdSoftware { get; set; }
    public string? NomeSoftware { get; set; }

    // DISPOSITIVO
    public string? DeviceFingerprint { get; set; }
    public string? DeviceInfo { get; set; }
    public bool? DeviceIsActive { get; set; }
    public DateTime? DeviceActivatedAt { get; set; }
}
