using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicencaApi.Models
{
    [Table("acessos_new")]
    public class AcessoNewModel
    {
        [Key]
        [Column("id_acesso_new")]
        public int? idAcessosNew { get; set; }
        [Column("data_hora")]
        public DateTime DataHora { get; set; }
        [Column("id_licenca")]
        public string idLicenca { get; set; } = string.Empty;
        [Column("mac_address")]
        public string macaddress { get; set; } = string.Empty;
        [Column("processador")]  
        public string processador { get; set; } = string.Empty;
        [Column("software")]
        public string software { get; set; } = string.Empty;
        [Column("external_IP")]
        public string externalIP { get; set; } = string.Empty;

    }
}