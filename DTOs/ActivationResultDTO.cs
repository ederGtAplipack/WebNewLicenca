namespace LicencaApi.DTOs
{
    public class ActivationResultDTO
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; }
        public int? NumLic { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public int RemainingSlots { get; set; }
    }
}
