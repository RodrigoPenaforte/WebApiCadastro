using Microsoft.AspNetCore.Mvc;
using WebApiCadastro.Models.Responses;
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
        public async Task<ActionResult> BuscarTodoUsuarios()
        {
            var usuario = await _usuarioService.BuscarTodos();
            return Ok(usuario);
        }

        [HttpGet("usuarioId/{id}")]
        public async Task<ActionResult<ResponseModel<UsuarioModel>>>  BuscarUsuarioId(int id)
        {
            var usuarioId = await _usuarioService.BuscarPorId(id);
            return usuarioId;
        }


    }
}