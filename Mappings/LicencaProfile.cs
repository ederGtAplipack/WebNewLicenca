using AutoMapper;
using LicencaApi.DTOs;
using LicencaApi.Models;

namespace LicencaApi.Mappings
{
    public class LicencaProfile : Profile
    {
        public LicencaProfile()
        {
            CreateMap<CriarLicencaDTO, LicencaModel>()
                .ForMember(dest => dest.DataAtivacao, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Ativa"));

            CreateMap<AtualizarLicencaDTO, LicencaModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<AtivacaoDispositivoRequestDTO, LicencaModel>()
                .ForMember(dest => dest.Nome_Computador, opt => opt.MapFrom(src => src.NomeComputador))
                .ForMember(dest => dest.Tipo_Pc, opt => opt.MapFrom(src => src.TipoPc));

        }
    }
}
