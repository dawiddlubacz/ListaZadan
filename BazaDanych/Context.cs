using ListaZadan.Modele;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaZadan.BazaDanych
{
    public class Context : DbContext
    {
        public DbSet<Uzytkownik> Uzytkownicy { get; set; }
        public DbSet<Zadanie> Zadania { get; set; }
        public DbSet<Kategoria> Kategorie { get; set; }
        public Context(DbContextOptions<Context> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\ListaZadan;Initial Catalog=ListaZadan;");
            }
        }
    }
}
