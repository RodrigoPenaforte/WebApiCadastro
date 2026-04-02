using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCadastro.Services.Senha
{
    public interface ISenhaService
    {
        void CriarSenhaHash(string? senha, out byte[] senhaHash, out byte[] senhaSalt);
    }
}