using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Persistence.Database;
using Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Implementation
{
    public class UsuarioAtividadeHorasRepository : EntityRepositoryBase<UsuarioAtividadeHoras>, IUsuarioAtividadeHorasRepository
    {
        public UsuarioAtividadeHorasRepository(DeparterContext dbContext) : base(dbContext)
        {
        }

        public List<UsuarioAtividadeHoras> FindFull()
        {
            return DbContext.UsuarioAtividadeHoras.Include(x => x.Usuario)
                                                            .ThenInclude(x => x.UsuarioHorasConfiguracaos)
                                                        .Include(x => x.Atividade)
                                                            .ThenInclude(x => x.AtividadeCategorias)
                                                                .ThenInclude(x => x.Categoria)
                                                        .ToList();
        }


        public List<UsuarioAtividadeHoras> FindFullByCondition(Expression<Func<UsuarioAtividadeHoras, bool>> value)
        {
            return DbContext.UsuarioAtividadeHoras.Include(x => x.Usuario)
                                                            .ThenInclude(x => x.UsuarioHorasConfiguracaos)
                                                        .Include(x => x.Atividade)
                                                            .ThenInclude(x => x.AtividadeCategorias)
                                                                .ThenInclude(x => x.Categoria)
                                                        .Where(value)
                                                        .ToList();
        }

    }
}
