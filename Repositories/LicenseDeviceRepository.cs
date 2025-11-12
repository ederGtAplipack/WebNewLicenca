using LicencaApi.Data;
using LicencaApi.DTOs;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using LicencaApi.MSSQL.Builders;
using Microsoft.EntityFrameworkCore;

namespace LicencaApi.Repositories
{
    public class LicenseDeviceRepository : ILicenseDeviceRepository
    {
        private readonly LicencaDbContext _context;
        private readonly ILogger<LicenseDeviceRepository> _logger;

        public LicenseDeviceRepository(LicencaDbContext context, ILogger<LicenseDeviceRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<LicencaDeviceDTO>> GetAllDevicesAsync()
        {         
            _logger.LogInformation("Fetching all license devices from the database.");
            var devices = LicencaDispositivoSqlBuilder.GetAllDevicesQuery();
            return await _context.Set<LicencaDeviceDTO>().FromSqlRaw(devices).ToListAsync();
        }
    }
}
