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
        public DbSet<Conteudo> Conteudo { get; set; }
        public DbSet<Historia> Historias { get; set; }
        public DbSet<Local> Local { get; set; }
        public DbSet<Utilizador> Utilizador { get; set; }

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
