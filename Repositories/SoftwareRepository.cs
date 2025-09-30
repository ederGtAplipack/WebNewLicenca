using LicencaApi.Data;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LicencaApi.Repositories
{
    public class SoftwareRepository : ISoftwareRepository
    {
        private readonly ILogger<SoftwareRepository> _logger;
        private readonly LicencaDbContext _context;
        public SoftwareRepository(ILogger<SoftwareRepository> logger, LicencaDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<SoftwareModel>> GetAllSoftwaresAsync()
        {
            _logger.LogInformation("Passando pelo Repository do GetAllSoftwaresAsync.");
            return await _context.Softwares.ToListAsync();
        }

        public async Task<SoftwareModel?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Passando pelo Repository do GetByIdAsync.");
            return await _context.Softwares.FindAsync(id);
        }
        public async Task CreateNewSoftware(SoftwareModel software)
        {
            _logger.LogInformation("Adicionando um novo software ao contexto.");
            await _context.Softwares.AddAsync(software);
            _logger.LogInformation("Novo software adicionado ao contexto com sucesso.");

        }

        public void Update(SoftwareModel existingSoftware)
        {
            _logger.LogInformation("Atualizando software existente no contexto.");
            _context.Entry(existingSoftware).State = EntityState.Modified;
        }

        public async void Delete(SoftwareModel existingSoftware)
        {
            _logger.LogInformation("Removendo software do contexto.");
            _context.Softwares.Remove(existingSoftware);
            _logger.LogInformation("Software removido do contexto com sucesso.");
        }
    }
}
