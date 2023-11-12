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
    public class EquipeUsuarioMapping : IEntityTypeConfiguration<EquipeUsuario>
    {
        public void Configure(EntityTypeBuilder<EquipeUsuario> builder)
        {
            builder.HasKey(x => new { x.UsuarioId, x.EquipeId });
        }
    }
}
