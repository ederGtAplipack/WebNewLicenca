namespace LicencaApi.DTOs
{
    public class AtualizarAnagraficaDTO
    {
        public int IdAnagrafica { get; set; }
        public string? RazaoSocial { get; set; } = string.Empty;
        public string? NomeFantasia { get; set; } = string.Empty;
        public string? Contato { get; set; } = string.Empty;
        public string? Cep { get; set; } = string.Empty;
        public string? Endereco { get; set; } = string.Empty;
        public string? Bairro { get; set; } = string.Empty;
        public string? Cidade { get; set; } = string.Empty;
        public string? Uf { get; set; } = string.Empty;
        public string? Cnpj { get; set; } = string.Empty;
        public string? Ie { get; set; } = string.Empty;
        public string? Telefone { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public int? IdRevenda { get; set; }
        public string? Senha { get; set; } = string.Empty;
    }
}
