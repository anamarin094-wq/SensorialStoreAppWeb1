using Microsoft.EntityFrameworkCore;
using SensorialStore.DAL.Entities;

namespace SensorialStore.DAL
{
    public class SensorialDbContext : DbContext
    {
        // El constructor que recibirá la conexión desde el proyecto Web
        public SensorialDbContext(DbContextOptions<SensorialDbContext> options) : base(options)
        {
        }

        // Aquí se mapea las entidades a las tablas de SQL Server
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Marca> Marcas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Esto asegura que Entity Framework sepa exactamente cómo se llaman las tablas de la BD
            modelBuilder.Entity<Producto>().ToTable("Productos");
            modelBuilder.Entity<Categoria>().ToTable("Categorias");
            modelBuilder.Entity<Marca>().ToTable("Marcas");
        }
    }
}