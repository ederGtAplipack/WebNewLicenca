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
        public string idAcessosNew { get; set; } = string.Empty;
        [Column("data_hora")]
        public DateTime DataHora { get; set; }
        [Column("id_licenca")]
        public string idLicenca { get; set; }
        [Column("mac_address")]
        public string macaddress { get; set; }
        [Column("processador")]  
        public string processador { get; set; }
        [Column("software")]
        public string software { get; set; }
        [Column("external_IP")]
        public string externalIP { get; set; }

    }
}