using LicencaApi.Data;
using LicencaApi.DTOs;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using LicencaApi.MSSQL.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LicencaApi.Repositories
{
    public class ContratoRepository : IContratoRepository
    {
        private readonly LicencaDbContext _context;
        private readonly ILogger<ClienteRepository> _logger;

        public ContratoRepository(LicencaDbContext context, ILogger<ClienteRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ContratoModel?> BuscarPorIdContrato(int id)
        {
            _logger.LogInformation("Passando pelo Repository do BuscarPorIdContrato{id}.", id);
            return await _context.Contratos.FindAsync(id);
        }

        public async Task<IEnumerable<ContratoDetalhadoDTO>> BuscarTodasContratos()
        {
            _logger.LogInformation("Passando pelo ClienteRepository BuscarTodasContrato.");
            //FIM
            var sql = ContratoSqlBuilder.GetAllContratosDetailsSql();
            return await _context.Set<ContratoDetalhadoDTO>().FromSqlRaw(sql).ToListAsync();
        }

        public async Task CreateNewContrato(ContratoModel contratoModel)
        {
            _logger.LogInformation("Passando pelo Repository do CreateNewContrato.");
            await _context.Contratos.AddAsync(contratoModel);
        }

        public void AtualizarContratto(ContratoModel contratoModel)
        {
            _logger.LogInformation("Passando pelo ContatroRepository.AtualizarContrato.");
            _context.Entry(contratoModel).State = EntityState.Modified;
        }

        public async Task DeleteContratoAsync(ContratoModel contrato)
        {

            _logger.LogInformation("Passando pelo Repository do DeleteContrato.");
            _context.Contratos.Remove(contrato);

        }

        public async Task<ContratoModel?> BuscarContratoComLicenca(int idContrato)
        {
            _logger.LogInformation("Passando pelo Repository do BuscarContratoComLicenca.");
            //return _context.Contratos.FindAsync(idContrato).AsTask();
            //var sql = ContratoSqlBuilder.GetContratoWithLicencaSql();
            return await _context.Contratos
                .Include(c => c.Licencas)
                .FirstOrDefaultAsync(c => c.IdContrato == idContrato);
        }
      
    }
}