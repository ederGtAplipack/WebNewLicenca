using System.ComponentModel.DataAnnotations.Schema;

namespace LicencaApi.Models
{
    [Table("licencadispositivo")]
    public class LicencaDispositivoModel
    {
        public int idDispositivo { get; set; }
        public int numLic { get; set; }
        public string DeviceFingerprint { get; set; }
        public string DeviceInfo { get; set; }
        public DateTime? ActivatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastSeenAt { get; set; }
        public int IsActive { get; set; } = 1;
    }
}
