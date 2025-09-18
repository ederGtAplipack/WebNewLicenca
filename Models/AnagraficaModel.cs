using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicencaApi.Models
{
    [Table("anagrafica")]
    public class AnagraficaModel
    {
        [Key]
        [Column("idanagrafica")]
        public int IdAnagrafica { get; set; }

        [Column("razaoSocial")]
        public string? RazaoSocial { get; set; } = string.Empty;

        [Column("nomeFantasia")]
        public string? NomeFantasia { get; set; } = string.Empty;

        [Column("contato")]
        public string? Contato { get; set; } = string.Empty;

        [Column("cep")]
        public string? Cep { get; set; } = string.Empty;

        [Column("endereco")]
        public string? Endereco { get; set; } = string.Empty;

        [Column("bairro")]
        public string? Bairro { get; set; } = string.Empty;

        [Column("cidade")]
        public string? Cidade { get; set; } = string.Empty;

        [Column("uf")]
        public string? Uf { get; set; } = string.Empty;

        [Column("cnpj")]
        public string? Cnpj { get; set; } = string.Empty;

        [Column("ie")]
        public string? Ie { get; set; } = string.Empty;

        [Column("telefone")]
        public string? Telefone { get; set; } = string.Empty;

        [Column("email")]
        public string? Email { get; set; } = string.Empty;

        [Column("idrevenda")]
        public int? IdRevenda { get; set; }

        [Column("Senha")]
        public string? Senha { get; set; } = string.Empty;

        // FK
        /*[ForeignKey("IdRevenda")]
        public virtual RevendaModel? revenda { get; set; }*/
    }
}
