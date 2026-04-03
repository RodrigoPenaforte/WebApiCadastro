using AutoMapper;
using WebApiCadastro.Dtos.UsuariosDtos;
using WebApiCadastro.Models.Usuarios;

namespace WebApiCadastro.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UsuarioModel, UsuarioPostDtos>().ReverseMap();
            CreateMap<UsuarioModel, UsuarioOutPutDto>().ReverseMap();
            CreateMap<UsuarioPutDtos, UsuarioModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DataCriacao, opt => opt.Ignore());
        }
    }
}