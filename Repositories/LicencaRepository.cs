using LicencaApi.Data;
using LicencaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LicencaApi.Repositories
{
    public class LicencaRepository : ILicencaRepository
    {
        private readonly LicencaDbContext _context;
        public LicencaRepository(LicencaDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<LicencaModel>> BuscarTodasAsync()
        {
            return await _context.Licenca.ToListAsync();
        }
        public async Task<LicencaModel?> BuscarPorIdAsync(int id)
        {
            return await _context.Licenca.FindAsync(id);
        }
        public async Task<IEnumerable<LicencaModel>> BuscarAtivasAsync()
        {
            return await _context.Licenca.Where(l => l.Attivo).ToListAsync();
        }
        public async Task<LicencaModel> CriarAsync(LicencaModel model)
        {
            _context.Licenca.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<bool> AtualizarAsync(LicencaModel model)
        {
            _context.Entry(model).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await LicencaExists(model.NumLic))
                    return false;
                throw;
            }
        }
        public async Task<bool> DesativarAsync(int id)
        {
            var licenca = await BuscarPorIdAsync(id);
            if (licenca == null) return false;
            licenca.Attivo = false;
            return await AtualizarAsync(licenca);
        }
        private async Task<bool> LicencaExists(int id)
        {
            return await _context.Licenca.AnyAsync(e => e.NumLic == id);
        }
    }
}

