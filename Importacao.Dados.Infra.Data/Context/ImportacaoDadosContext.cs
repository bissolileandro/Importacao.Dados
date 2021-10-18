using Importacao.Dados.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Importacao.Dados.Infra.Data.Context
{
    public class ImportacaoDadosContext : DbContext
    {
        public ImportacaoDadosContext(string connectionString)
            :base(GetOptions(connectionString))
        {

        }
        public ImportacaoDadosContext(DbContextOptions<ImportacaoDadosContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigurarProduto(modelBuilder);

        }
#region [Confiugurartions]
        public void ConfigurarProduto(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(x =>
            {
                x.ToTable("Produto");
                x.HasKey(p => new { p.Id });
                x.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd();
                x.Property(p => p.Nome).HasColumnName("Nome").HasMaxLength(50);
            });
        }
#endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
    }
}
