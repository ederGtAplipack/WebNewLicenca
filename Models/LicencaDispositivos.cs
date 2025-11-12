using System.ComponentModel.DataAnnotations;

namespace LicencaApi.Models
{
    public class LicencaDispositivos
    {
        [Key]
        public int idDispositivo { get; set; }
        public int numLic { get; set; }
        public string DeviceFingerprint { get; set; } = string.Empty;
        public string DeviceInfo { get; set; } = string.Empty;
        public DateTime ActivatedAt { get; set; }
        public DateTime LastSeenAt { get; set; }
        public bool IsActive { get; set; }
        
    }
}
