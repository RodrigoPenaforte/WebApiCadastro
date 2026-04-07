using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCadastro.Models.Usuarios;

namespace WebApiCadastro.Services.Senha
{
    public interface ISenhaService
    {
        void CriarSenhaHash(string? senha, out byte[] senhaHash, out byte[] senhaSalt);
        bool VerificarSenhaHash(string? senha, byte[] senhaHash, byte[] senhaSalt);
        string CriarToken(UsuarioModel usuarioModel);
    }
}