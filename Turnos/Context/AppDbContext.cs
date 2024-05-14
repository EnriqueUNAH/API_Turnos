using Microsoft.EntityFrameworkCore;
using Turnos.Models;

namespace Turnos.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura ID_USUARIO como la clave primaria de la entidad Usuarios
            modelBuilder.Entity<Usuarios>().HasKey(u => u.ID_USUARIO);

            // Otras configuraciones de modelo pueden ir aquí
        }
    }
}
