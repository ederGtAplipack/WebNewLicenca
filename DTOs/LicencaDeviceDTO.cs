namespace LicencaApi.DTOs
{
    public class LicencaDeviceDTO
    {
        public int IdDispositivo { get; set; }
        public string DeviceFingerprint { get; set; }
        public string DeviceInfo { get; set; }
        public DateTime? ActivatedAt { get; set; }
        public DateTime? LastSeenAt { get; set; }
        public bool IsActive { get; set; }
    }
}
