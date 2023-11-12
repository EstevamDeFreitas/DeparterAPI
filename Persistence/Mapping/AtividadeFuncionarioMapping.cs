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
    internal class AtividadeUsuarioMapping : IEntityTypeConfiguration<AtividadeUsuario>
    {
        public void Configure(EntityTypeBuilder<AtividadeUsuario> builder)
        {
            builder.HasKey(x => new { x.AtividadeId, x.UsuarioId });
        }
    }
}
