using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiCadastro.Dtos.UsuariosDtos;
using WebApiCadastro.Models.Responses;
using WebApiCadastro.Models.Usuarios;
using WebApiCadastro.Services.Usuario;

namespace WebApiCadastro.Controllers.UsuarioController
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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

        [HttpGet("{id}", Name = "ObterUsuario")]
        public async Task<ActionResult<ResponseModel<UsuarioModel>>> BuscarUsuarioId(int id)
        {
            var usuarioId = await _usuarioService.BuscarPorId(id);
            return usuarioId;
        }


        [HttpPut]
        public async Task<ActionResult<ResponseModel<UsuarioOutPutDto>>> AtualizarUsuario(UsuarioPutDtos usuarioPutDtos)
        {
            var usuario = await _usuarioService.EditarUsuario(usuarioPutDtos);

            if (!usuario.Status)
            {
                return BadRequest(usuario);
            }

            return Ok(usuario);


        }

        [HttpDelete("DeletarUsuario/{id}")]
        public async Task<ActionResult<ResponseModel<UsuarioModel>>> DeletarUsuario(int id)
        {
            var usuarioId = await _usuarioService.DeletarUsuario(id);
            return Ok(usuarioId);
        }

    }
}