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
    public class DepartamentoFuncionarioMapping : IEntityTypeConfiguration<DepartamentoFuncionario>
    {
        public void Configure(EntityTypeBuilder<DepartamentoFuncionario> builder)
        {
            builder.HasKey(x => new { x.FuncionarioId, x.DepartamentoId });
        }
    }
}
