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
        private readonly Lazy<IAtividadeRepository> _atividadeRepository;
        private readonly Lazy<ICategoriaRepository> _categoriaRepository;
        private readonly Lazy<IAtividadeCategoriaRepository> _atividadeCategoriaRepository;
        private readonly Lazy<IAtividadeFuncionarioRepository> _atividadeFuncionarioRepository;

        public RepositoryWrapper(DeparterContext context)
        {
            _context = context;
            _funcionarioRepository = new Lazy<IFuncionarioRepository>(() => new FuncionarioRepository(context));
            _atividadeCategoriaRepository = new Lazy<IAtividadeCategoriaRepository> (() => new AtividadeCategoriaRepository(context));
            _atividadeFuncionarioRepository = new Lazy<IAtividadeFuncionarioRepository> (() => new AtividadeFuncionarioRepository(context));
            _categoriaRepository = new Lazy<ICategoriaRepository>(() => new CategoriaRepository(context));
            _atividadeRepository = new Lazy<IAtividadeRepository>(() => new AtividadeRepository(context));
        }

        public IFuncionarioRepository FuncionarioRepository => _funcionarioRepository.Value;

        public IAtividadeCategoriaRepository AtividadeCategoriaRepository => _atividadeCategoriaRepository.Value;

        public IAtividadeFuncionarioRepository AtividadeFuncionarioRepository => _atividadeFuncionarioRepository.Value;

        public IAtividadeRepository AtividadeRepository => _atividadeRepository.Value;

        public ICategoriaRepository CategoriaRepository => _categoriaRepository.Value;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
