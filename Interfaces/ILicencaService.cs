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
        Task<IEnumerable<LicencaModel>> BuscarTodasAsync();
        Task<IEnumerable<AnagraficaModel>> BuscarTodasAnagrafica();

        Task<LicencaModel?> BuscarPorIdLicenca(int id);
        Task<AnagraficaModel?> BuscarPorIdAnagrafica(int id);

		Task<LicencaModel?> CriarAsync(CriarLicencaDTO dto);
        Task<bool> AtualizarAsync(int id, AtualizarLicencaDTO dto);
        Task<IEnumerable<LicencaModel?>> BuscarAtivasAsync();
        Task<bool> DesativarAsync(int id);
        Task<AtivacaoDispositivoResponseDTO> ProcessarAtivacaoDispositivoAsync(AtivacaoDispositivoRequestDTO request);
        Task<AtivacaoDispositivoResponseDTO> BuscarLicencaExistenteAsync(AtivacaoDispositivoRequestDTO request);
        Task<bool> DeletarAsync(int id);


        Task<IEnumerable<LicencaDetalhadaDTO>>  ObterTodasComDetalhesAsync();
        /* O método CriarNovaLinhaAsync é responsável por criar uma nova licença detalhada
         * a partir dos dados fornecidos no DTO (Data Transfer Object) LicencaDetalhadaDTO.
         * Ele retorna a licença criada ou null se a criação falhar.
         */
        Task<AnagraficaModel?> CriarNovaAnagrafica(CriarAnagraficaDTO dto);

        Task<LicencaModel?> CriarNewLicenca(LicencaDetalhadaDTO dto);
    }
}