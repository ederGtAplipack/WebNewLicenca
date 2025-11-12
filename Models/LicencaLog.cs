namespace LicencaApi.Models
{
    public class LicencaLog
    {
        public int idLog { get; set; }
        public int numLic { get; set; }
        public string Chave { get; set; }
        public string Endpoint { get; set; }
        public string RequestPayload { get; set; }
        public int ResponseCode { get; set; }
        public string ClienteIp { get; set; }
        public DateTime CreatedAt { get; set; }
        public string mensagem { get; set; }        
    }
}
