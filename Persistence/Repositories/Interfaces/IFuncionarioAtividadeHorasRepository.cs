using Domain.Entities;
using Persistence.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Interfaces
{
    public interface IFuncionarioAtividadeHorasRepository : IEntityRepositoryBase<FuncionarioAtividadeHoras>
    {
        List<FuncionarioAtividadeHoras> FindFullByCondition(Expression<Func<FuncionarioAtividadeHoras, bool>> value);
    }

    
}
