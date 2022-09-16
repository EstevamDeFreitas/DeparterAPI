using Persistence.Database;
using Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Implementation
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DeparterContext _context;

        public RepositoryWrapper(DeparterContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
