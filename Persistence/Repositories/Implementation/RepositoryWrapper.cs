﻿using Domain.Entities;
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
        private readonly Lazy<IEntityRepositoryBase<Departamento>> _departamentoRepository;
        private readonly Lazy<IRepositoryBase<DepartamentoFuncionario>> _departamentoFuncionarioRepository;
        private readonly Lazy<IEntityRepositoryBase<AtividadeCheck>> _atividadeCheckRepository;
        private readonly Lazy<IFuncionarioAtividadeHorasRepository> _funcionarioAtividadeHorasRepository;
        private readonly Lazy<IFuncionarioHorasConfiguracaoRepository> _funcionarioHorasConfiguracaoRepository;

        public RepositoryWrapper(DeparterContext context)
        {
            _context = context;
            _funcionarioRepository = new Lazy<IFuncionarioRepository>(() => new FuncionarioRepository(context));
            _atividadeCategoriaRepository = new Lazy<IAtividadeCategoriaRepository> (() => new AtividadeCategoriaRepository(context));
            _atividadeFuncionarioRepository = new Lazy<IAtividadeFuncionarioRepository> (() => new AtividadeFuncionarioRepository(context));
            _categoriaRepository = new Lazy<ICategoriaRepository>(() => new CategoriaRepository(context));
            _atividadeRepository = new Lazy<IAtividadeRepository>(() => new AtividadeRepository(context));
            _departamentoRepository = new Lazy<IEntityRepositoryBase<Departamento>>(() => new EntityRepositoryBase<Departamento>(context));
            _departamentoFuncionarioRepository = new Lazy<IRepositoryBase<DepartamentoFuncionario>>(() => new RepositoryBase<DepartamentoFuncionario>(context));
            _atividadeCheckRepository = new Lazy<IEntityRepositoryBase<AtividadeCheck>>(() => new EntityRepositoryBase<AtividadeCheck>(context));
            _funcionarioAtividadeHorasRepository = new Lazy<IFuncionarioAtividadeHorasRepository>(() => new FuncionarioAtividadeHorasRepository(context));
            _funcionarioHorasConfiguracaoRepository = new Lazy<IFuncionarioHorasConfiguracaoRepository>(() => new FuncionarioHorasConfiguracaoRepository(context));
        }

        public IFuncionarioRepository FuncionarioRepository => _funcionarioRepository.Value;

        public IAtividadeCategoriaRepository AtividadeCategoriaRepository => _atividadeCategoriaRepository.Value;

        public IAtividadeFuncionarioRepository AtividadeFuncionarioRepository => _atividadeFuncionarioRepository.Value;

        public IAtividadeRepository AtividadeRepository => _atividadeRepository.Value;

        public ICategoriaRepository CategoriaRepository => _categoriaRepository.Value;

        public IEntityRepositoryBase<Departamento> DepartamentoRepository => _departamentoRepository.Value;
        public IRepositoryBase<DepartamentoFuncionario> DepartamentoFuncionarioRepository => _departamentoFuncionarioRepository.Value;
        public IEntityRepositoryBase<AtividadeCheck> AtividadeCheckRepository => _atividadeCheckRepository.Value;

        public IFuncionarioHorasConfiguracaoRepository FuncionarioHorasConfiguracaoRepository => _funcionarioHorasConfiguracaoRepository.Value;

        public IFuncionarioAtividadeHorasRepository AtividadeHorasRepository => _funcionarioAtividadeHorasRepository.Value;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
