using System.ComponentModel.DataAnnotations;

namespace WebApiCadastro.Dtos.LoginDtos
{
    public class UsuarioLoginDtos
    {
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite a senha")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 20 caracteres")]
        public string Senha { get; set; } = string.Empty;
    }
}