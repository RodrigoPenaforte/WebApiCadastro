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
        }
    }
}