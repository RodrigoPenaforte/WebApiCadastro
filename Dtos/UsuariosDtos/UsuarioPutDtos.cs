using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCadastro.Dtos.UsuariosDtos
{
    public class UsuarioPutDtos
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o Usuário")]
        public string? Usuario { get; set; }
        [Required(ErrorMessage = "Digite o Nome Completo")]
        public string? NomeCompleto { get; set; }
        [Required(ErrorMessage = "Digite o Email")]
        public string? Email { get; set; }
    }
}