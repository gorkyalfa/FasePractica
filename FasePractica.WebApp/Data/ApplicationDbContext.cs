using FasePractica.WebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FasePractica.WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Convenio> Convenios { get; set; }
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
