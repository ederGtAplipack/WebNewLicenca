namespace LicencaApi.DTOs
{
    public class LicencaLogDto
    {
        public int IdLog { get; set; }
        public int? NumLic { get; set; }
        public string Chave { get; set; }
        public string Endpoint { get; set; }
        public string RequestPayload { get; set; }
        public int ResponseCode { get; set; }
        public string ClientIp { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Mensagem { get; set; }
    }
}
