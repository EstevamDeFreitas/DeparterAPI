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
        private readonly Lazy<IFuncionarioRepository> _funcionarioRepository;

        public RepositoryWrapper(DeparterContext context)
        {
            _context = context;
            _funcionarioRepository = new Lazy<IFuncionarioRepository>(() => new FuncionarioRepository(context));
        }

        public IFuncionarioRepository FuncionarioRepository => _funcionarioRepository.Value;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
