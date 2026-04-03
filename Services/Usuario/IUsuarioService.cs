using Azure;
using WebApiCadastro.Dtos.UsuariosDtos;
using WebApiCadastro.Models.Responses;
using WebApiCadastro.Models.Usuarios;

namespace WebApiCadastro.Services.Usuario
{
    public interface IUsuarioService
    {
       public Task<ResponseModel<List<UsuarioModel>>> BuscarTodos();
       public Task<ResponseModel<UsuarioModel>> BuscarPorId(int id);
       public Task<ResponseModel<UsuarioModel>> DeletarUsuario(int id);
       public Task<ResponseModel<UsuarioModel>> CriarUsuario(UsuarioPostDtos usuarioPostDtos);
       public Task<ResponseModel <UsuarioOutPutDto>> EditarUsuario(UsuarioPutDtos usuarioPutDtos);


       
    }
}