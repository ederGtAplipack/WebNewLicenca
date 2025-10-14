using LicencaApi.DTOs;
using LicencaApi.Models;
using System.Collections;
using System.ComponentModel;

namespace LicencaApi.Interfaces
{
    /*A interface ILicencaService define um contrato para serviços
     * relacionados ao gerenciamento de licenças (LicencaModel).
     * Ela especifica os métodos que qualquer implementação deve 
     * fornecer, mas não contém lógica — apenas as assinaturas.*/
    public interface ILicencaService
    {
        //Task<IEnumerable<LicencaModel>> BuscarTodasAsync();

        //Task<LicencaModel?> BuscarPorIdLicenca(int id);

		Task<LicencaModel?> CriarAsync(CriarLicencaDTO dto);
        //Task<bool> AtualizarAsync(int id, AtualizarLicencaDTO dto);
        //Task<IEnumerable<LicencaModel?>> BuscarAtivasAsync();
        //Task<bool> DesativarAsync(int id);
        //Task<AtivacaoDispositivoResponseDTO> ProcessarAtivacaoDispositivoAsync(AtivacaoDispositivoRequestDTO request);
        //Task<AtivacaoDispositivoResponseDTO> BuscarLicencaExistenteAsync(AtivacaoDispositivoRequestDTO request);
        //Task<bool> DeletarAsync(int id);


        Task<IEnumerable<LicencaDetalhadaDTO>>  ObterTodasComDetalhesAsync();

        //Task<LicencaModel?> CriarNewLicenca(LicencaDetalhadaDTO dto);

        Task<LicencaDTO> CreateAsync(CriarLicencaDTO dto);
        Task<ActivationResultDTO> ActivateAsync(ActivateLicenseDTO licenseDTO, string clienteIp = null);
        Task<ValidationResultDTO> ValidateAsync(ValidateLicenseDTO licenseDTO, string clienteIp = null);

        Task<bool> UpdateStatusAsync(int numLic, string status, string reason = null);
        Task<IEnumerable<LicencaDeviceDTO>> GetDevicesAsync(int numLic);
        Task<IEnumerable<LicencaLogDto>> GetLogsAsync(int numLic, DateTime? from = null, DateTime? to = null);
        Task<GenerateMultipleResultDTO> GenerateMultipleAsync(GenerateMultipleLicensesDTO dto);
    }
}