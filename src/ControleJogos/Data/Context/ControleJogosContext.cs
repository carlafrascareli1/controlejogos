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

        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<Amigo> Amigos { get; set; }
    }
}
