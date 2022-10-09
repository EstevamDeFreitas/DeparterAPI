﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Interfaces
{
    public interface IFuncionarioRepository : IEntityRepositoryBase<Funcionario>
    {
        IQueryable<Funcionario> GetFuncionariosFromEmails(List<string> emails);
    }
}
