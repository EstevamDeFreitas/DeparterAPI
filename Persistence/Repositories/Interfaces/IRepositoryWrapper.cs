using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        IFuncionarioRepository FuncionarioRepository { get; }
        IAtividadeCategoriaRepository AtividadeCategoriaRepository { get; }
        IAtividadeFuncionarioRepository AtividadeFuncionarioRepository { get; }
        IAtividadeRepository AtividadeRepository { get; }
        ICategoriaRepository CategoriaRepository { get; }
        IEntityRepositoryBase<Departamento> DepartamentoRepository { get; }
        void Save();
    }
}
