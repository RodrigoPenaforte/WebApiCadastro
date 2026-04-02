using System.ComponentModel.DataAnnotations;
namespace WebApiCadastro.Dtos.UsuariosDtos
{
    public class UsuarioPostDtos
    {
        [StringLength(20, MinimumLength = 4, ErrorMessage = "O usuário deve ter entre 4 e 20 caracteres")]
        public string Usuario { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite o Nome Completo")]
        public string NomeCompleto { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite o Email")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite a senha")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 20 caracteres")]
        public string Senha { get; set; } = string.Empty;
        [Required(ErrorMessage = "Confirme a senha")]
        [Compare("Senha", ErrorMessage = "As senhas não conferem")]
        public string ConfirmaSenha { get; set; } = string.Empty;
    }
}