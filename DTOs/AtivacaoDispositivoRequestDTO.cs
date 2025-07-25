namespace LicencaApi.DTOs
{
    /*Este DTO representará os dados enviados pelo cliente para ativar/validar a licença.*/
    public class AtivacaoDispositivoRequestDTO
    {
        public string MacAddress { get; set; } = string.Empty;
        public string Software { get; set; } = string.Empty;
        public string IdLicencaChave { get; set; } = string.Empty; // Serial implícita
        public string Processador { get; set; } = string.Empty;
        public string SistemaOp { get; set; } = string.Empty;
        public string TipoPc { get; set; } = string.Empty; // Host Type
        public string NomeComputador { get; set; } = string.Empty; // Hostname
        public string Ip { get; set; } = string.Empty; // External IP do cliente
        // Adicione IdCliente se o software cliente já o souber.
        public int? IdCliente { get; set; }
        public string? IdContrato { get; set; } // Referência ao contrato associado, se aplicável
        public string? IdLicenca { get; set; } = string.Empty; // Referência à licença associada, se aplicável  
        public string? numLic { get; set; } = string.Empty; // Número da licença, se aplicável
        public string? idAcessosNew { get; set; } = string.Empty; // Identificador de acessos, se necessário
        public DateTime DateTime { get; set; } = DateTime.UtcNow; // Data e hora da ativação, padrão para o momento atual
        public string? VersaoSoftware { get; set; } = string.Empty; // Versão do software cliente, se necessário
        public string? externalIp { get; set; } = string.Empty; // IP externo do cliente, se necessário
    }
}
