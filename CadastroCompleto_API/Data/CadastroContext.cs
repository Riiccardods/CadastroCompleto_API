using Microsoft.EntityFrameworkCore;
using CadastroCompleto_API.Models;

namespace CadastroCompleto_API.Data
{
    public class CadastroContext : DbContext
    {
        public CadastroContext(DbContextOptions<CadastroContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}