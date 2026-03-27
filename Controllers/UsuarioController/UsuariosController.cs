using Microsoft.AspNetCore.Mvc;
using WebApiCadastro.Models.Usuarios;
using WebApiCadastro.Services.Usuario;

namespace WebApiCadastro.Controllers.UsuarioController
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
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