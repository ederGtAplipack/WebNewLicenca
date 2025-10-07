using LicencaApi.Data;
using LicencaApi.DTOs;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using LicencaApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace LicencaApi.Controllers.V1
{
    /* * O LicencaController é responsável por gerenciar as operações relacionadas às licenças.
     * Ele permite buscar, criar, atualizar e desativar licenças, além de processar ativações de dispositivos.
     * As ações são implementadas usando o padrão RESTful, com métodos HTTP apropriados.
     */
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LicencaController_back : ControllerBase
    {
        private readonly ILicencaService _service;
        private readonly ILogger<LicencaController> _logger;
        private readonly LicencaDbContext _context;

        public LicencaController_back(ILicencaService service, ILogger<LicencaController> logger, LicencaDbContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }        
       
    }
}
