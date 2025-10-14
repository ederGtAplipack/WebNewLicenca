namespace LicencaApi.DTOs
{
    public class GenerateMultipleResultDTO
    {
        public int TotalRequested { get; set; }
        public int TotalCreated { get; set; }
        public List<LicencaDTO> CreatedLicenseKeys { get; set; } = new();
        public List<string> FailedLicenseKeys { get; set; } = new();
        public string Message => $"{TotalCreated} de {TotalRequested} licenças criadas com sucesso.";
    }
}
