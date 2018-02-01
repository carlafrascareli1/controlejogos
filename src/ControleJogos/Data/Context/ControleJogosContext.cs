using ControleJogos.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleJogos.Data.Context
{
    public class ControleJogosContext : DbContext
    {
        public ControleJogosContext(DbContextOptions options)
            :base (options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmprestimoJogo>()
                .HasKey(bc => new { bc.JogoID, bc.EmprestimoID });

            modelBuilder.Entity<EmprestimoJogo>()
                .HasOne(bc => bc.Jogo)
                .WithMany(b => b.EmprestimoJogo)
                .HasForeignKey(bc => bc.JogoID);

            modelBuilder.Entity<EmprestimoJogo>()
                .HasOne(bc => bc.Emprestimo)
                .WithMany(c => c.EmprestimoJogo)
                .HasForeignKey(bc => bc.EmprestimoID);

            modelBuilder.Entity<Emprestimo>()
                .HasOne(bc => bc.Amigo)
                .WithMany(c => c.Emprestimo)
                .HasForeignKey(bc => bc.AmigoID);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<Amigo> Amigos { get; set; }
        public DbSet<Emprestimo> Emprestimo { get; set; }
        public DbSet<EmprestimoJogo> EmprestimoJogo { get; set; }
    }
}
