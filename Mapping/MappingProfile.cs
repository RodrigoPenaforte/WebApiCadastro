using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApiCadastro.Dtos.UsuariosDtos;
using WebApiCadastro.Models.Usuarios;

namespace WebApiCadastro.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UsuarioModel, UsuarioDtos>().ReverseMap();
        }
    }
}