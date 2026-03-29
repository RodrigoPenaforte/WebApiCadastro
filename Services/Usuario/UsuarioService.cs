using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApiCadastro.Data;
using WebApiCadastro.Dtos.UsuariosDtos;
using WebApiCadastro.Models.Responses;
using WebApiCadastro.Models.Usuarios;

namespace WebApiCadastro.Services.Usuario
{
    public class UsuarioService : IUsuarioService
    {
        public readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
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
    }
}