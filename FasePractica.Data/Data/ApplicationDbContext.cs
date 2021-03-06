﻿using FasePractica.Data.Models.Global;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FasePractica.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Instituto> Institutos { get; set; }
        public DbSet<UsuarioPorInstituto> UsuarioPorInstitutos { get; set; }
    }
}
