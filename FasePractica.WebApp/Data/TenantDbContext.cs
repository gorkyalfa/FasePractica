using FasePractica.WebApp.Models;
using FasePractica.WebApp.Services;
using Microsoft.EntityFrameworkCore;

namespace FasePractica.WebApp.Data
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Comentar la declaración de la variable "tenant" y el "if" cuando quiera crear un nuevo schema  
            var tenant = TenantStorage.Instance().Tenant;
            if (!string.IsNullOrEmpty(tenant))
                builder.HasDefaultSchema(TenantStorage.Instance().Tenant);

            // Descomentar y remplazar el valor "t1" con el nombre del schema que desea construir
            // Crear migracion y actualizar la base de datos
            //builder.HasDefaultSchema("t1");

            builder.Entity<Persona>()
                .HasDiscriminator<string>("TipoPersona")
                .HasValue<Contacto>("Contacto")
                .HasValue<Estudiante>("Estudiante")
                .HasValue<Tutor>("Tutor");

            builder.Entity<Contacto>()
                .HasOne(e => e.Empresa)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Nota>()
                .HasOne(e => e.Proyecto)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Proyecto>()
                .HasOne(e => e.TutorAcademico)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Proyecto>()
                .HasOne(e => e.TutorEmpresarial)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(builder);
        }
        
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Conversacion> Conversaciones { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Semestre> Semestres { get; set; }
        public DbSet<Tutor> Tutores { get; set; }
    }
}