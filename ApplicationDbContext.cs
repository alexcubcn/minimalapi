using Microsoft.EntityFrameworkCore;
using MinimalAPIP.Entidades;

namespace MinimalAPIP
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Genero> Generos { get; set; }
    }
}
