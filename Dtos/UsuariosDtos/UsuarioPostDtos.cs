using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCadastro.Dtos.UsuariosDtos
{
    public class UsuarioPostDtos
    {
        [Required(ErrorMessage = "Digite o Usuário")]
        public string? Usuario { get; set; }
        [Required(ErrorMessage = "Digite o Nome Completo")]
        public string? NomeCompleto { get; set; }
        [Required(ErrorMessage = "Digite o Email")]
        public string? Email { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime DataAlteracao { get; set; }
        [Required(ErrorMessage = "Digite o Senha")]
        public string? Senha { get; set; }
        [Required(ErrorMessage = "Digite a confirmação da senha"), Compare("Senha", ErrorMessage = "As senhas são são ")]
        public string? ConfirmaSenha { get; set; }
    }
}