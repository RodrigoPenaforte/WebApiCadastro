using WebApiCadastro.Dtos.UsuariosDtos;
using WebApiCadastro.Models.Responses;
using WebApiCadastro.Models.Usuarios;

namespace WebApiCadastro.Services.Usuario
{
    public interface IUsuarioService
    {
       public Task<ResponseModel<List<UsuarioModel>>> BuscarTodos();
    }
}