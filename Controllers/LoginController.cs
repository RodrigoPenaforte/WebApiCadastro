using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiCadastro.Dtos.UsuariosDtos;
using WebApiCadastro.Models.Responses;
using WebApiCadastro.Services.Usuario;

namespace WebApiCadastro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<LoginController> _logger;
        private readonly IMapper _mapper;

        public LoginController(IUsuarioService usuarioService, ILogger<LoginController> logger, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UsuarioOutPutDto>> RegistrarUsuario(UsuarioPostDtos usuarioPostDtos)
        {
            var usuario = await _usuarioService.CriarUsuario(usuarioPostDtos);

            if (!usuario.Status)
            {
                return BadRequest(usuario);
            }

            var usuarioOutPut = _mapper.Map<UsuarioOutPutDto>(usuario.Dados);

            var response = new ResponseModel<UsuarioOutPutDto>
            {
              Dados = usuarioOutPut,
              Mensagem = "Usuário cadastrado com sucesso...",
            };


            return CreatedAtRoute("ObterUsuario", new { id = usuario.Dados.Id }, response);
        }
    }
}