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
    public class UsuarioRepository : EntityRepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DeparterContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Usuario> GetUsuariosFromEmails(List<string> emails)
        {
            return DbContext.Usuarios.Where(x => emails.Contains(x.Email)).AsNoTracking();
        }
    }
}
