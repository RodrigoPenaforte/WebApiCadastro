using Microsoft.EntityFrameworkCore;
using WebApiCadastro.Models.Auditorias;
using WebApiCadastro.Models.Usuarios;
namespace WebApiCadastro.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }

    }
}



