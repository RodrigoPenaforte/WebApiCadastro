using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApiCadastro.Data;
using WebApiCadastro.Dtos.LoginDtos;
using WebApiCadastro.Dtos.UsuariosDtos;
using WebApiCadastro.Models.Responses;
using WebApiCadastro.Models.Usuarios;
using WebApiCadastro.Services.Auditorias;
using WebApiCadastro.Services.Senha;

namespace WebApiCadastro.Services.Usuario
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;
        private readonly ISenhaService _senhaService;
        private readonly IMapper _mapper;
        private readonly IAuditoriaService _auditoriaService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UsuarioService(AppDbContext context, ISenhaService senhaService, IMapper mapper, IAuditoriaService auditoriaService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _senhaService = senhaService;
            _mapper = mapper;
            _auditoriaService = auditoriaService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel<UsuarioModel>> BuscarPorId(int id)
        {
            ResponseModel<UsuarioModel> response = new();

            try
            {
                var usuarioId = await _context.Usuarios.FirstOrDefaultAsync(userId => userId.Id == id);

                if (usuarioId is null)
                {
                    response.Mensagem = "Usuário não encontrado...";
                    return response;
                }

                response.Dados = usuarioId;
                response.Mensagem = $"Usuário {usuarioId.NomeCompleto} encontrado..";
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }

        }

        public async Task<ResponseModel<List<UsuarioModel>>> BuscarTodos()
        {
            ResponseModel<List<UsuarioModel>> response = new();

            try
            {
                var usuarios = await _context.Usuarios.ToListAsync();

                if (usuarios.Count() == 0)
                {
                    response.Mensagem = "Nenhum usuário cadastrado!";
                    return response;
                }

                response.Dados = usuarios;
                response.Mensagem = "Usuário localizado com sucesso";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<UsuarioModel>> CriarUsuario(UsuarioPostDtos usuarioPostDtos)
        {
            ResponseModel<UsuarioModel> response = new();

            try
            {
                if (await ExisteEmailOuUsuario(usuarioPostDtos))
                {
                    response.Mensagem = "Email ou usuário já cadastrado";
                    response.Status = false;
                    return response;
                }

                _senhaService.CriarSenhaHash(usuarioPostDtos.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                UsuarioModel usuario = _mapper.Map<UsuarioModel>(usuarioPostDtos);
                // Destino         //Origem

                usuario.SenhaHash = senhaHash;
                usuario.SenhaSalt = senhaSalt;
                usuario.DataCriacao = DateTime.UtcNow;
                usuario.DataAlteracao = DateTime.UtcNow;

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                response.Mensagem = "Usuário Cadastrado com sucesso..";
                response.Dados = usuario;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<UsuarioModel>> DeletarUsuario(int id)
        {
            ResponseModel<UsuarioModel> response = new();

            try
            {
                var usuarioId = await _context.Usuarios.FirstOrDefaultAsync(i => i.Id == id);
                if (usuarioId is null)
                {
                    response.Mensagem = "Usuário não encontrado...";
                    return response;
                }

                response.Dados = usuarioId;

                _context.Remove(response.Dados);
                await _context.SaveChangesAsync();

                response.Mensagem = "Usuário deletado com sucesso...";

                string dadosAntes = JsonConvert.SerializeObject(usuarioId);

                var userId = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

                await _auditoriaService.RegistrarAuditoriaAsync("Remoção", userId, $"Antes: {dadosAntes}");
                return response;


            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<UsuarioOutPutDto>> EditarUsuario(UsuarioPutDtos usuarioPutDtos)
        {
            ResponseModel<UsuarioOutPutDto> response = new();

            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == usuarioPutDtos.Id);

                if (usuario is null)
                {
                    response.Mensagem = "Usuário não encontrado";
                    return response;
                }

                string dadosAntes = JsonConvert.SerializeObject(usuario);

                _mapper.Map(usuarioPutDtos, usuario); //  Origem (usuarioPutDtos) -> Destino (usuario) 

                usuario.DataAlteracao = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                response.Dados = _mapper.Map<UsuarioOutPutDto>(usuario);  // Destino (UsuarioOutPutDto) <- Origem (usuario)
                response.Mensagem = "Usuário atualizado com sucesso..";

                string dadosDepois = JsonConvert.SerializeObject(usuario);
                var usuarioId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
                await _auditoriaService.RegistrarAuditoriaAsync("Atualização", usuarioId, $"Antes: {dadosAntes} - Depois {dadosDepois}");


                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<UsuarioOutPutDto>> LoginUsuario(UsuarioLoginDtos usuarioLoginDtos)
        {
            ResponseModel<UsuarioOutPutDto> response = new();

            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(userBancoEmail => userBancoEmail.Email == usuarioLoginDtos.Email);

                if (usuario is null)
                {
                    response.Mensagem = "Email do Usuário não encontrado";
                    response.Status = false;
                    return response;
                }

                if (!_senhaService.VerificarSenhaHash(usuarioLoginDtos.Senha, usuario.SenhaHash, usuario.SenhaSalt))
                {
                    response.Mensagem = "Senha do Usuário não encontrado";
                    response.Status = false;
                    return response;
                }

                var token = _senhaService.CriarToken(usuario);
                usuario.Token = token;

                _context.Update(usuario);
                await _context.SaveChangesAsync();

                response.Dados = _mapper.Map<UsuarioOutPutDto>(usuario);
                response.Mensagem = "Usuário logado com sucesso...";
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        private async Task<bool> ExisteEmailOuUsuario(UsuarioPostDtos usuarioPostDtos)
        {
            return await _context.Usuarios.AnyAsync(u =>
                u.Email == usuarioPostDtos.Email ||
                u.Usuario == usuarioPostDtos.Usuario);
        }
    }
}