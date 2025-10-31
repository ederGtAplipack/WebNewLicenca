using Microsoft.Ajax.Utilities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;
using System.Web.Helpers;

namespace LicencaApi.Models
{
    [Table("licencalog")]
    public class LicencaLogModel
    {
       [Column("idlog")]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public int idLog { get; set; }
       public int? numLic { get; set; }
       public string? chave { get; set; }            
       public string? endPoint { get; set; }
       public string? RequestPayload { get; set; }    
       public int? responseCode { get; set; }
       public string? ClienteIp { get; set; }
       public DateTime createdAt { get; set; }
       public string? mensagem { get; set; }
    }
}
