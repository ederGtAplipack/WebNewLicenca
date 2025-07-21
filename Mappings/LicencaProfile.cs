using AutoMapper;
using LicencaApi.DTOs;
using LicencaApi.Models;

namespace LicencaApi.Mappings
{
    public class LicencaProfile : Profile
    {
        public LicencaProfile()
        {
            CreateMap<CriarLicencaDTO, LicencaModel>();

            CreateMap<AtualizarLicencaDTO, LicencaModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
