using Domain.Entities;
using Persistence.Database;
using Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Implementation
{
    public class AtividadeRepository : RepositoryBase<Atividade>, IAtividadeRepository
    {
        public AtividadeRepository(DeparterContext dbContext) : base(dbContext)
        {
        }
    }
}
