namespace LicencaApi.DTOs
{
    public class GenerateMultipleLicensesDTO
    {
        public int IdContrato { get; set; }
        public int IdCliente { get; set; }
        public int IdSoftware { get; set; }
        public int Quantidade { get; set; } = 1;
        public DateTime Scade { get; set; }
        public int MaxDevices { get; set; } = 1;
    }
}
