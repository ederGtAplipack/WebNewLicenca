using LicencaApi.DTOs;
using LicencaApi.Models;
using System.Collections;
using System.ComponentModel;

namespace LicencaApi.Services
{
    /*A interface ILicencaService define um contrato para serviços
     * relacionados ao gerenciamento de licenças (LicencaModel).
     * Ela especifica os métodos que qualquer implementação deve 
     * fornecer, mas não contém lógica — apenas as assinaturas.*/
    public interface ILicencaService
    {
        Task<IEnumerable<LicencaModel>> BuscarTodasAsync();
        Task<LicencaModel?> BuscarPorIdAsync(int id);
        Task<LicencaModel> CriarAsync(CriarLicencaDTO dto);
        Task<bool> AtualizarAsync(int id, AtualizarLicencaDTO dto);
        Task<bool> DesativarAsync(int id);
        Task<IEnumerable<LicencaModel>> BuscarAtivasAsync();
        Task<AtivacaoDispositivoResponseDTO> ProcessarAtivacaoDispositivoAsync(
            AtivacaoDispositivoRequestDTO request);

    }
}