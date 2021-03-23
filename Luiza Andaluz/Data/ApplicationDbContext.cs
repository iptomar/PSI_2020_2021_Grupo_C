using LuizaAndaluz.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Luiza_Andaluz.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Conteudo> Areas { get; set; }
        public DbSet<Historia> Ficheiros { get; set; }
        public DbSet<Local> Comentarios { get; set; }
        public DbSet<Utilizador> Downloads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
