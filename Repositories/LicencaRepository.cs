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

        /*public async Task<IEnumerable<LicencaModel>> BuscarAtivasAsync()
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
        }*/

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

        public async Task<LicencasChaveModel?> GetLicencaChaveByChaveAsync(string chave)
        {
            _logger.LogInformation("Passando pelo Repository do GetLicencaChaveByChaveAsync {chave}.", chave);
            return await _context.LicencasChave.FirstOrDefaultAsync(lc => lc.Chave == chave);
        }

        public async Task<LicencaModel> GetLicencaByIdLicencaChaveAsync(int idLicencaChave)
        {
            _logger.LogInformation("Passando pelo Repository do GetLicencaByIdLicencaChaveAsync {idLicencaChave}.", idLicencaChave);
            return await _context.Licenca.FirstOrDefaultAsync(l => l.IdLicencaChave == idLicencaChave);
        }

        public async Task<LicencaModel> GetLicencaByNumLicAsync(int numLic)
        {
            _logger.LogInformation("Passando pelo Repository do GetLicencaByNumLicAsync {numLic}.", numLic);
            return await _context.Licenca.FirstOrDefaultAsync(l => l.NumLic == numLic);
        }

        public async Task<int> CountActiveDevicesAsync(int numLic)
        {
            return await _context.LicencaDispositivo.CountAsync(d => d.numLic == numLic && d.IsActive == 1);
        }

        public async Task<LicencaDispositivoModel> GetDeviceByFingerprintAsync(int numLic, string deviceFingerprint)
        {
            var device = await _context.LicencaDispositivo
                .FirstOrDefaultAsync(d => d.numLic == numLic && d.DeviceFingerprint == deviceFingerprint);            
            return device;
        }

        public async Task<LicencaDispositivoModel> AddDeviceAsync(LicencaDispositivoModel device)
        {
            await _context.LicencaDispositivo.AddAsync(device);
            return device;
        }

        public async Task<LicencaDispositivoModel> UpdateDeviceAsync(LicencaDispositivoModel device)
        {
           _context.LicencaDispositivo.Update(device);
            return device;
        }

        public async Task LogAsync(LicencaLogModel logEntry)
        {
            _context.LicencaLog.Add(logEntry);
            await _context.SaveChangesAsync();
            return;
        }


        public async Task<IEnumerable<LicencaDispositivoModel>> GetDevicesAsync(int numLic)
        {
            _context.LicencaDispositivo.Where(d => d.numLic == numLic);
            return await _context.LicencaDispositivo.Where(d => d.numLic == numLic).ToListAsync();
        }

        public async Task<IEnumerable<LicencaLogModel>> GetLogsAsync(int numLic, DateTime? from = null, DateTime? to = null)
        {
            _context.LicencaLog.Where(l => l.numLic == numLic);
            await _context.SaveChangesAsync();
            var query = _context.LicencaLog.AsQueryable();
            query = query.Where(l => l.numLic == numLic);
            if (from.HasValue)
            {
                query = query.Where(l => l.createdAt >= from.Value);
            }
            if (to.HasValue)
            {
                query = query.Where(l => l.createdAt <= to.Value);
            }
            return await query.ToListAsync();

        }

        public async Task UpdateLicencaAsync(LicencaModel licenca)
        {
           _context.Licenca.Update(licenca);
            return;
        }

        public async Task<LicencasChaveModel> CreateLicencaChaveAsync(LicencasChaveModel licencasChave)
        {
            _context.LicencasChave.Add(licencasChave);
            await _context.SaveChangesAsync();
            return licencasChave;
        }

        public async Task<LicencaModel> CreateLicencaAsync(LicencaModel licenca)
        {   
            _context.Licenca.Add(licenca);
            await _context.SaveChangesAsync();
            return licenca;
        }

        public async Task UpdateLicencaChaveAsync(LicencasChaveModel chave)
        {
            _context.LicencasChave.Update(chave);
            return;
        }

        public async Task VincularHardwareAsync(int numLic, string mac, string tipoPc, string nomeComputador, string processador, string ip, string sistemaOp)
        {
            var licenca = await _context.Licenca.FirstOrDefaultAsync(l => l.NumLic == numLic);
            if (licenca == null)
                throw new KeyNotFoundException($"Licença #{numLic} não encontrada para vincular hardware.");

            licenca.MacAddress = mac;
            licenca.TipoPc = tipoPc;
            licenca.NomeComputador = nomeComputador;
            licenca.Processador = processador;
            licenca.ip = ip;
            licenca.SistemaOp = sistemaOp;
            licenca.Status = "Active";
            licenca.Attivo = 1;
            licenca.DataAtivacao = DateTime.UtcNow;

            _context.Licenca.Update(licenca);
            await _context.SaveChangesAsync();

        } 
        public void AtualizarLicenca(LicencaModel model)
        {
            _logger.LogInformation("Passando pelo Repository do Update.");
            _context.Entry(model).State = EntityState.Modified;

        }
    }
}
