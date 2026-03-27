using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCadastro.Dtos.UsuariosDtos
{
    public class UsuarioDtos
    {
        public string? Usuario { get; set; }
        public string? NomeCompleto { get; set; }
        public string? Email { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime DataAlteracao { get; set; }
    }
}