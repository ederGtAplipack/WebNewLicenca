namespace LicencaApi.DTOs
{
    public class ValidationResultDTO
    {
        public bool Valid { get; set; }
        public string Status { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string Message { get; set; }
    }
}
