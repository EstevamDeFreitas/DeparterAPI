using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Database
{
    public class DeparterContext : DbContext
    {
        public DbSet<Funcionario> Funcionarios { get; set; }

        #region Atividades
        public DbSet<Atividade> Atividades { get; set; }
        public DbSet<AtividadeCategoria> AtividadeCategorias { get; set; }
        public DbSet<AtividadeFuncionario> AtividadeFuncionarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<AtividadeCheck> AtividadeChecks { get; set; }
        #endregion

        #region Departamentos
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<DepartamentoFuncionario> DepartamentoFuncionarios { get; set; }
        #endregion

        #region Horas
        public DbSet<FuncionarioAtividadeHoras> FuncionarioAtividadeHoras { get; set; }
        public DbSet<FuncionarioHorasConfiguracao> FuncionarioHorasConfiguracaos { get; set; }
        #endregion
        public DeparterContext(DbContextOptions<DeparterContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AtividadeFuncionarioMapping());
            modelBuilder.ApplyConfiguration(new AtividadeCategoriaMapping());
            modelBuilder.ApplyConfiguration(new AtividadeMapping());
            modelBuilder.ApplyConfiguration(new DepartamentoFuncionarioMapping());
            modelBuilder.ApplyConfiguration(new FuncionarioAtividadeHorasMapping());
        }
    }
}
