using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Mapping
{
    internal class AtividadeFuncionarioMapping : IEntityTypeConfiguration<AtividadeFuncionario>
    {
        public void Configure(EntityTypeBuilder<AtividadeFuncionario> builder)
        {
            builder.HasKey(x => new { x.AtividadeId, x.FuncionarioId });
        }
    }
}
