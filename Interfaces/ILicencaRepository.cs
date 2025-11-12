using AutoMapper;
using LicencaApi.DTOs;
using LicencaApi.Models;
using System.ComponentModel;

namespace LicencaApi.Interfaces
{
    public interface ILicencaRepository
    {
        Task<IEnumerable<LicencaModel>> BuscarTodasAsync();
        Task<LicencaModel> BuscarPorIdAsync(int id);
        Task CriarAsync(LicencaModel model);
        //Task CreateSql(LicencaModel licencaDetalhadaDTO);

        void AtualizarAsync(LicencaModel model);
        Task SalvarAsync();
        //Task<IEnumerable<LicencaModel>> BuscarAtivasAsync();
        //Task<bool> DesativarAsync(int id);
        Task<IEnumerable<LicencaDetalhadaDTO>> ObterTodasComDetalhesAsync();

        Task<LicencasChaveModel?> GetLicencaChaveByChaveAsync(string chave);
        Task<LicencaModel> GetLicencaByIdLicencaChaveAsync(int idLicencaChave);

        Task<LicencaModel> GetLicencaByNumLicAsync(int numLic);
        Task<int> CountActiveDevicesAsync(int numLic);

        /* busca um dispositivo (LicencaDispositivo) utilizando uma impressão digital (fingerprint) 
         * juntamente com o número da licença*/
        Task<LicencaDispositivoModel> GetDeviceByFingerprintAsync(int numLic, string deviceFingerprint);
        Task<LicencaDispositivoModel> AddDeviceAsync(LicencaDispositivoModel device);
        Task<LicencaDispositivoModel> UpdateDeviceAsync(LicencaDispositivoModel device);

        Task LogAsync(LicencaLogModel logEntry);
        Task<IEnumerable<LicencaDispositivoModel>> GetDevicesAsync(int numLic);
        /* método que busca logs de licença dentro de uma faixa de datas opcional.*/
        Task<IEnumerable<LicencaLogModel>> GetLogsAsync(int numLic, DateTime? from = null, DateTime? to = null);

        Task UpdateLicencaAsync(LicencaModel licenca);
        Task<LicencasChaveModel> CreateLicencaChaveAsync(LicencasChaveModel licencasChave);
        Task<LicencaModel> CreateLicencaAsync(LicencaModel licenca);
        Task UpdateLicencaChaveAsync(LicencasChaveModel chave);
        //Task <IEnumerator<LicencaModel>> BuscarPorIdLicenca(int id);
        void AtualizarLicenca(LicencaModel model);
    }
}
