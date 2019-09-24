using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var db = new LivrosContext())
            {
                db.Livros.Add(new Livro 
                    { Titulo = "Domain-Driven Design: Tackling Complexity in the Heart of Software", 
                        Autor = "Eric Evans", AnoPublicacao = 2003 });
                db.Livros.Add(new Livro 
                    { Titulo = "Agile Principles, Patterns, and Practices in C#",
                        Autor = "Robert C. Martin", AnoPublicacao = 2006 });

                db.SaveChanges();
                
                var livros = db.Livros.ToList();
                Console.WriteLine("--------------------RESULTADOS--------------------");
                //db.Livros.ForEachAsync(x => Console.WriteLine($"Título: {x.Titulo} | Autor: {x.Autor}"));
                foreach(var l in livros)
                     Console.WriteLine($"Título: {l.Titulo} | Autor: {l.Autor}");
            }
        }
    }

    public class LivrosContext : DbContext
    {
        public DbSet<Livro> Livros { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFCore.Demo;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Livro>()
                .ToTable("Livro");
            
            modelBuilder.Entity<Livro>()
                .HasKey(p => p.LivroId);

            modelBuilder.Entity<Livro>()
                .Property(p => p.Titulo)
                .HasColumnType("varchar(100)");
        }
    }

    public class Livro
        {
            public int LivroId { get; set; }
            public string Titulo { get; set; }
            public string Autor { get; set; }
            public int AnoPublicacao { get; set; }
        }
}
