namespace LicencaApi.DTOs
{
    /*Este DTO será a resposta da API para o cliente, contendo a chave de licença e seu status.*/
    public class AtivacaoDispositivoResponseDTO
    {
        public string ChaveLicenca { get; set; } = string.Empty; // A chave final validada/gerada
        public DateTime DataExpiracao { get; set; }
        public string StatusLicenca { get; set; } = string.Empty; // Ex: "Ativa", "Expirada", "Pendente Analise", "Negada"
        public string Mensagem { get; set; } = string.Empty; // Mensagens de sucesso ou erro

    }
}
