using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface IServiceWrapper
    {
        IFuncionarioService FuncionarioService { get; }
        ITokenService TokenService { get; }
        ILoginService LoginService { get; }
        ICategoriaService CategoriaService { get; }
        IAtividadeService AtividadeService { get; }
        IDepartamentoService DepartamentoService { get; }
    }
}
