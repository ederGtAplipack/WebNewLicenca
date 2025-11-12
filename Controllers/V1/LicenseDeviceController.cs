using LicencaApi.Interfaces;
using LicencaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LicencaApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LicenseDeviceController : ControllerBase
    {
        private readonly ILogger<LicenseDeviceController> _logger;
        private readonly ILicenseDeviceService _licenseDeviceService;

        public LicenseDeviceController(ILogger<LicenseDeviceController> logger, ILicenseDeviceService licenseDevice)
        {
            _logger = logger;
            _licenseDeviceService = licenseDevice;
        }

        [HttpGet("licenseDeviceStatus")]
        public async Task<IActionResult> GetStatus()
        {
            try
            {
                var status = await _licenseDeviceService.GetLicenseDeviceStatusAsync();
                return Ok(status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter status do dispositivo de licença.");
                return StatusCode(500, "Erro interno do servidor.");

            }
        }
    }
}
