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
        private readonly IMapper _mapper;

        public UsuarioService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseModel<List<UsuarioModel>>> BuscarTodos()
        {
            ResponseModel<List<UsuarioModel>> response = new();

            try
            {
                var usuarios = await _context.Usuarios.ToListAsync();

                if (usuarios.Count() == 0)
                    response.Mensagem = "Nenhum usuário cadastrado!";

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