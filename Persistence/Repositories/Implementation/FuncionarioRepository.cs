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
    public class FuncionarioRepository : EntityRepositoryBase<Funcionario>, IFuncionarioRepository
    {
        public FuncionarioRepository(DeparterContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Funcionario> GetFuncionariosFromEmails(List<string> emails)
        {
            return DbContext.Funcionarios.Where(x => emails.Contains(x.Email)).AsNoTracking();
        }
    }
}
