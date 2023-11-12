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
        IUsuarioRepository UsuarioRepository { get; }
        IAtividadeCategoriaRepository AtividadeCategoriaRepository { get; }
        IAtividadeUsuarioRepository AtividadeUsuarioRepository { get; }
        IAtividadeRepository AtividadeRepository { get; }
        ICategoriaRepository CategoriaRepository { get; }
        IEntityRepositoryBase<Equipe> EquipeRepository { get; }
        IRepositoryBase<EquipeUsuario> EquipeUsuarioRepository { get; }
        IEntityRepositoryBase<AtividadeCheck> AtividadeCheckRepository { get; }
        IUsuarioHorasConfiguracaoRepository UsuarioHorasConfiguracaoRepository { get; }
        IUsuarioAtividadeHorasRepository AtividadeHorasRepository { get; }
        void Save();
    }
}
