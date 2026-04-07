namespace WebApiCadastro.Dtos.UsuariosDtos
{
    public class UsuarioOutPutDto
    {
        public int Id { get; set; }
        public string? Usuario { get; set; }
        public string? NomeCompleto { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
    }
}