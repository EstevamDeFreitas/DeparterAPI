using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Implementation
{
    public class AtividadeRepository : EntityRepositoryBase<Atividade>, IAtividadeRepository
    {
        public AtividadeRepository(DeparterContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Atividade> FindFullById(Guid id)
        {
            return DbContext.Atividades.Include(x => x.AtividadeCategorias)
                                            .ThenInclude(x => x.Categoria)
                                        .Include(x => x.AtividadeFuncionarios)
                                            .ThenInclude(x => x.Funcionario)
                                        .Include(x => x.Atividades)
                                        .Include(x => x.AtividadeChecks)
                                        .Include(x => x.Departamento)
                                        .Where(x => x.Id == id).AsNoTracking();
        }
    }
}
