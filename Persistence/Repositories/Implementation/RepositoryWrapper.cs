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
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DeparterContext _context;
        private readonly Lazy<IUsuarioRepository> _usuarioRepository;
        private readonly Lazy<IAtividadeRepository> _atividadeRepository;
        private readonly Lazy<ICategoriaRepository> _categoriaRepository;
        private readonly Lazy<IAtividadeCategoriaRepository> _atividadeCategoriaRepository;
        private readonly Lazy<IAtividadeUsuarioRepository> _atividadeUsuarioRepository;
        private readonly Lazy<IEntityRepositoryBase<Equipe>> _equipeRepository;
        private readonly Lazy<IRepositoryBase<EquipeUsuario>> _equipeUsuarioRepository;
        private readonly Lazy<IEntityRepositoryBase<AtividadeCheck>> _atividadeCheckRepository;
        private readonly Lazy<IUsuarioAtividadeHorasRepository> _usuarioAtividadeHorasRepository;
        private readonly Lazy<IUsuarioHorasConfiguracaoRepository> _usuarioHorasConfiguracaoRepository;

        public RepositoryWrapper(DeparterContext context)
        {
            _context = context;
            _usuarioRepository = new Lazy<IUsuarioRepository>(() => new UsuarioRepository(context));
            _atividadeCategoriaRepository = new Lazy<IAtividadeCategoriaRepository> (() => new AtividadeCategoriaRepository(context));
            _atividadeUsuarioRepository = new Lazy<IAtividadeUsuarioRepository> (() => new AtividadeUsuarioRepository(context));
            _categoriaRepository = new Lazy<ICategoriaRepository>(() => new CategoriaRepository(context));
            _atividadeRepository = new Lazy<IAtividadeRepository>(() => new AtividadeRepository(context));
            _equipeRepository = new Lazy<IEntityRepositoryBase<Equipe>>(() => new EntityRepositoryBase<Equipe>(context));
            _equipeUsuarioRepository = new Lazy<IRepositoryBase<EquipeUsuario>>(() => new RepositoryBase<EquipeUsuario>(context));
            _atividadeCheckRepository = new Lazy<IEntityRepositoryBase<AtividadeCheck>>(() => new EntityRepositoryBase<AtividadeCheck>(context));
            _usuarioAtividadeHorasRepository = new Lazy<IUsuarioAtividadeHorasRepository>(() => new UsuarioAtividadeHorasRepository(context));
            _usuarioHorasConfiguracaoRepository = new Lazy<IUsuarioHorasConfiguracaoRepository>(() => new UsuarioHorasConfiguracaoRepository(context));
        }

        public IUsuarioRepository UsuarioRepository => _usuarioRepository.Value;

        public IAtividadeCategoriaRepository AtividadeCategoriaRepository => _atividadeCategoriaRepository.Value;

        public IAtividadeUsuarioRepository AtividadeUsuarioRepository => _atividadeUsuarioRepository.Value;

        public IAtividadeRepository AtividadeRepository => _atividadeRepository.Value;

        public ICategoriaRepository CategoriaRepository => _categoriaRepository.Value;

        public IEntityRepositoryBase<Equipe> EquipeRepository => _equipeRepository.Value;
        public IRepositoryBase<EquipeUsuario> EquipeUsuarioRepository => _equipeUsuarioRepository.Value;
        public IEntityRepositoryBase<AtividadeCheck> AtividadeCheckRepository => _atividadeCheckRepository.Value;

        public IUsuarioHorasConfiguracaoRepository UsuarioHorasConfiguracaoRepository => _usuarioHorasConfiguracaoRepository.Value;

        public IUsuarioAtividadeHorasRepository AtividadeHorasRepository => _usuarioAtividadeHorasRepository.Value;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
