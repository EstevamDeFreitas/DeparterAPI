﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<Atividade> Atividades { get; set; }
        public DbSet<AtividadeCategoria> AtividadeCategorias { get; set; }
        public DbSet<AtividadeFuncionario> AtividadeFuncionarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DeparterContext(DbContextOptions<DeparterContext> options) : base(options)
        {
        }
    }
}
