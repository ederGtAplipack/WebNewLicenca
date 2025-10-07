using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicencaApi.Models
{
    //[Table("revenda_user")]
    public class RevendaUserModel
    {
        public int idRevenda { get; set; }
        public string idUser { get; set; }

        public RevendaModel Revenda { get; set; }
    }
}
