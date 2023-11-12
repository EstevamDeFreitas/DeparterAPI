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
        public DbSet<Usuario> Usuarios { get; set; }

        #region Atividades
        public DbSet<Atividade> Atividades { get; set; }
        public DbSet<AtividadeCategoria> AtividadeCategorias { get; set; }
        public DbSet<AtividadeUsuario> AtividadeUsuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<AtividadeCheck> AtividadeChecks { get; set; }
        #endregion

        #region Equipes
        public DbSet<Equipe> Equipes { get; set; }
        public DbSet<EquipeUsuario> EquipeUsuarios { get; set; }
        #endregion

        #region Horas
        public DbSet<UsuarioAtividadeHoras> UsuarioAtividadeHoras { get; set; }
        public DbSet<UsuarioHorasConfiguracao> UsuarioHorasConfiguracaos { get; set; }
        #endregion
        public DeparterContext(DbContextOptions<DeparterContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AtividadeUsuarioMapping());
            modelBuilder.ApplyConfiguration(new AtividadeCategoriaMapping());
            modelBuilder.ApplyConfiguration(new AtividadeMapping());
            modelBuilder.ApplyConfiguration(new EquipeUsuarioMapping());
        }
    }
}
