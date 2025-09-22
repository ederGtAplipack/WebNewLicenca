using AutoMapper;
using LicencaApi.DTOs;
using LicencaApi.Models;

namespace LicencaApi.Helpers
{
    public class LicencaMapper : Profile
    {
        public LicencaMapper()
        {
            CreateMap<CriarLicencaDTO, LicencaModel>();
            CreateMap<LicencaDetalhadaDTO, LicencaModel>();
            CreateMap<AtualizarLicencaDTO, LicencaModel>();
            CreateMap<LicencaModel, LicencaDTO>();
            CreateMap<CriarAnagraficaDTO, AnagraficaModel>();
            CreateMap<AtualizarAnagraficaDTO, AnagraficaModel>(); 
            CreateMap<CriarRevendaDTO, RevendaModel>();
            CreateMap<AtualizarRevendaDTO, RevendaModel>();
            CreateMap<CriarContratoDTO, ContratoModel>();
            CreateMap<AtualizarContratoDTO, ContratoModel>();
        }

    }
}
