using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Interfaces
{
    public interface IUsuarioRepository : IEntityRepositoryBase<Usuario>
    {
        IQueryable<Usuario> GetUsuariosFromEmails(List<string> emails);
    }
}
