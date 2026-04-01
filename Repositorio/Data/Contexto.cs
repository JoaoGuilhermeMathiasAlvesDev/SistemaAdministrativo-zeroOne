using Dominio;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Repositorio.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }
        public Contexto()
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Mensalidade> Mensalidades {  get; set; }
        public DbSet<AlunoTurma> AlunoTurmas { get; set; }
        public DbSet<Turma>Turmas { get; set; }

        public DbSet<Aluno> Alunos { get; set; }

        public DbSet<Chamada> Chamadas { get; set; }    
        public DbSet<ChamadaItem>chamadaItems { get; set; }

        public DbSet<Despesa> Despesas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Contexto).Assembly);


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Se o EF não recebeu a configuração da API, ele usa esta aqui:
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SistemaZeroOne;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

    }
}
