using Dapper;
using LicencaApi.Data;
using LicencaApi.DTOs;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using LicencaApi.MSSQL.Builders;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using LicencaApi.Services;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Net.WebSockets;

/*A classe LicencaRepository é responsável por realizar operações CRUD (Create, Read, Update, Delete) na tabela Licenca. Ela implementa os métodos definidos na interface ILicencaRepository, garantindo que outras partes do sistema possam interagir com os dados sem precisar conhecer os detalhes da implementação.

Os métodos são assíncronos (async) para melhorar a performance em cenários onde operações de I/O são realizadas, como consultas ao banco de dados. Isso evita bloqueios no thread principal e melhora a escalabilidade da aplicação.*/
namespace LicencaApi.Repositories
{
    public class LicencaRepository : ILicencaRepository
    {
        private readonly LicencaDbContext _context;
        private readonly ILogger<LicencaRepository> _logger;
        //private readonly string _connectionString; <- se usar Dapper

        public LicencaRepository(LicencaDbContext context, ILogger<LicencaRepository> logger, IConfiguration config )
        {
            _context = context;
            _logger = logger;
            //_connectionString = config.GetConnectionString("MySqlConnection"); <- se usar Dapper
        }

        public async Task<IEnumerable<LicencaModel>> BuscarTodasAsync()
        {
            _logger.LogInformation("Passando pelo LicencaRepository.");
            return await _context.Licenca.ToListAsync();
        }

        public async Task<IEnumerable<LicencaDetalhadaDTO>> ObterTodasComDetalhesAsync()
        {
            _logger.LogInformation("Passando pelo Repository do ObterTodasComDetalhesAsync.");

            // Usando EF Core para executar a consulta SQL diretamente e mapear para LicencaDetalhadaDTO
            var sql = LicencaSqlBuilder.GetAllWithDetailsSql();
            /* Usando FromSqlRaw para executar a consulta SQL e mapear os resultados para LicencaDetalhadaDTO */
            return await _context.Set<LicencaDetalhadaDTO>().FromSqlRaw(sql).ToListAsync();

            // Pega a conexão que o EF já gerencia
            /*var connection = _context.Database.GetDbConnection();
            // Se a conexão estiver fechada, abre ela
            if (connection.State == System.Data.ConnectionState.Closed)
                await connection.OpenAsync();
            // Usa Dapper para executar a consulta SQL                        
            var sql = LicencaSqlBuilder.GetAllWithDetailsSql();
            // Executa a consulta e mapeia os resultados para a lista de LicencaDetalhadaDTO
            return await connection.QueryAsync<LicencaDetalhadaDTO>(sql);  */

        }

        public async Task<LicencaModel?> BuscarPorIdAsync(int id)
        {
            _logger.LogInformation("Passando pelo Repository do BuscarPorIdAsync {id}.", id);
            return await _context.Licenca.FindAsync(id);
        }

        public async Task CriarAsync(LicencaModel model)
        {
            _logger.LogInformation("Passando pelo Repository do Create.");
            await _context.Licenca.AddAsync(model);
        }

        public async Task CreateSql(LicencaModel licencaDetalhadaDTO)
        {
            _logger.LogInformation("Criando Nova Licenca");
            var sql = LicencaSqlBuilder.CreateSql();
            await _context.Set<LicencaModel>().AddAsync(licencaDetalhadaDTO);
        }

        public void AtualizarAsync(LicencaModel model)
        {
            _logger.LogInformation("Passando pelo Repository do Update.");
            _context.Entry(model).State = EntityState.Modified;
        }

        private async Task<bool> LicencaExists(int id)
        {
            _logger.LogInformation("Verificando se a licença existe no Repository.");
            return await _context.Licenca.AnyAsync(e => e.NumLic == id);           
        }

        public async Task SalvarAsync()
        {
           //wait _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LicencaModel>> BuscarAtivasAsync()
        {
            // Aqui você pode implementar a lógica para buscar licenças ativas
            _logger.LogInformation("Passando pelo Repository das ativas.");
            return await _context.Licenca.Where(l => l.Attivo).ToListAsync();
        }

        public async Task<bool> DesativarAsync(int id)
        {
            var licenca = await BuscarPorIdAsync(id);
            if (licenca == null) return false;
            licenca.Attivo = false;
            return true;
        }

        public async Task<IEnumerable<AnagraficaModel>> BuscarTodasAnagrafica()
        {
            _logger.LogInformation("Passando pelo LicencaRepository.");
            return await _context.Anagrafica.ToListAsync();
        }

        public async Task<AnagraficaModel?> BuscarPorIdAnagrafica(int id)
        {
            _logger.LogInformation("Passando pelo Repository do BuscarPorIdAnagrafica {id}.", id);
            return await _context.Anagrafica.FindAsync(id);
        }

        public async Task CreateNewAnagrafica(AnagraficaModel anagraficaModel)
        {
            _logger.LogInformation("Passando pelo Repository do CreateNewAnagrafica.");
            await _context.Anagrafica.AddAsync(anagraficaModel);
        }
    }
}
