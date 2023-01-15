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
    public class DepartamentoAtividadeMapping : IEntityTypeConfiguration<DepartamentoAtividade>
    {
        public void Configure(EntityTypeBuilder<DepartamentoAtividade> builder)
        {
            builder.HasKey(x => new { x.AtividadeId, x.DepartamentoId });
        }
    }
}
