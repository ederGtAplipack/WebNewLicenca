using Microsoft.AspNetCore.Mvc;

namespace LicencaApi.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // Note: "licencas" em vez de "licenca"
    public class LicencasController : ControllerBase
    {
        [HttpGet("estatisticas")]
        public IActionResult GetEstatisticas()
        {
            return Ok(new { Versao = "2.0", Mensagem = "Listagem de licenças V2" });
        }
    }
}