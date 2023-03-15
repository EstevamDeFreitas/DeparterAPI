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
    public class FuncionarioAtividadeHorasRepository : EntityRepositoryBase<FuncionarioAtividadeHoras>, IFuncionarioAtividadeHorasRepository
    {
        public FuncionarioAtividadeHorasRepository(DeparterContext dbContext) : base(dbContext)
        {
        }

        public List<FuncionarioAtividadeHoras> FindFullByCondition(Expression<Func<FuncionarioAtividadeHoras, bool>> value)
        {
            return DbContext.FuncionarioAtividadeHoras.Include(x => x.Funcionario)
                                                            .ThenInclude(x => x.FuncionarioHorasConfiguracaos)
                                                        .Include(x => x.Atividade)
                                                        .Where(value)
                                                        .ToList();
        }

    }
}
