using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicencaApi.Models
{
    [Table("revenda")]
    public class RevendaModel
    {
        [Key]
        [Column("idrevenda")]
        public int IdRevenda { get; set; }
        [Column("razao_social")]
        public string RazaoSocial { get; set; } = string.Empty; 
        [Column("login")]
        public string Login { get; set; } = string.Empty;
        [Column("senha")]
        public string Senha { get; set; } = string.Empty;

    }
}
