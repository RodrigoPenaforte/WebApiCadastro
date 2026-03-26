namespace WebApiCadastro.Models.Usuarios
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string? Usuario { get; set; }
        public string? NomeCompleto { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public byte[]? SenhaHash { get; set; }
        public byte[]? SenhaSalt { get; set; }
    }
}