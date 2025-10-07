namespace LicencaApi.DTOs
{
    public class ActivateLicenseDTO
    {
        public string Chave { get; set; }
        public string DeviceFingerprint { get; set; }
        public DeviceInfoDTO DeviceInfo { get; set; }
    }
}
