namespace WebApiCadastro.Models.Auditorias
{
    public class Auditoria
    {
        public int Id { get; set; }
        public string Acao { get; set; }
        public DateTime Data { get; set; } = DateTime.UtcNow;
        public string UsuarioId { get; set; }
        public string DadosAlterados { get; set; }
    }
}