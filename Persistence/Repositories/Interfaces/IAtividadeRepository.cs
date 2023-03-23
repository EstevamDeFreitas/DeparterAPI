using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Interfaces
{
    public interface IAtividadeRepository : IEntityRepositoryBase<Atividade>
    {
        IQueryable<Atividade> FindFullById(Guid id);

        IQueryable<Atividade> FindAllFull(bool? isAdminSearch, Guid funcionarioId);
        void UpdateDatabaseAtividadesStatus();
    }
}
