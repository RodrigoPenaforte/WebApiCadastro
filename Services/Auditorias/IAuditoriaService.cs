using WebApiCadastro.Models.Auditorias;

namespace WebApiCadastro.Services.Auditorias
{
    public interface IAuditoriaService
    {
        Task RegistrarAuditoriaAsync(string acao, string usuarioId,string dadosAlterados);
        Task<List<Auditoria>> BuscarAuditoria();
    }
}