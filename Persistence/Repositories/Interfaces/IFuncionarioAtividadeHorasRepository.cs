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
    public interface IUsuarioAtividadeHorasRepository : IEntityRepositoryBase<UsuarioAtividadeHoras>
    {
        List<UsuarioAtividadeHoras> FindFull();
        List<UsuarioAtividadeHoras> FindFullByCondition(Expression<Func<UsuarioAtividadeHoras, bool>> value);
    }

    
}
