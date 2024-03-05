using Microsoft.EntityFrameworkCore;
using WebApplicationBE.Models;

namespace WebApplicationBE.Data
{
    public class ApplicationDbContext : DbContext
    {
     
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define las tablas que serán mapeadas a la base de datos
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Factura> Facturas { get; set; }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura las relaciones y restricciones de las entidades
            modelBuilder.Entity<Persona>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Factura>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Persona)
                .WithMany(p => p.Facturas)
                .HasForeignKey(f => f.IdPersona);

            // Configura las propiedades de las entidades
            modelBuilder.Entity<Persona>()
                .Property(p => p.Nombre)
                .IsRequired();

            modelBuilder.Entity<Persona>()
                .Property(p => p.ApellidoPaterno)
                .IsRequired();

            modelBuilder.Entity<Factura>()
                .Property(f => f.Fecha)
                .IsRequired();

            modelBuilder.Entity<Factura>()
                .Property(f => f.Monto)
                .IsRequired();
        }
    }
}
