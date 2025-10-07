using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicencaApi.Models
{
    [Table("revenda")]
    public class RevendaModel
    {
        [Key]
        [Column("idrevenda")]
        public int idRevenda { get; set; }
        [Column("razaoSocial")]
        public string razaoSocial { get; set; } = string.Empty; 
        
    }
}
