using Microsoft.AspNetCore.Mvc;
using WebApiCadastro.Models.Usuarios;
using WebApiCadastro.Services.Usuario;

namespace WebApiCadastro.Controllers.UsuarioController
{
    [Route("[controller]")]
    public class UsuariosController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodoUsuarios()
        {
            var usuario = await _usuarioService.BuscarTodos();
            return Ok(usuario);
        }


    }
}