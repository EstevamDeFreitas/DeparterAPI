using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface IFuncionarioService
    {
        FuncionarioDTO GetFuncionario(Guid id);
        List<FuncionarioDTO> GetFuncionarios();
        void CreateFuncionario(FuncionarioDTO funcionario);
        void UpdateFuncionario(FuncionarioDTO funcionario);
        void DeleteFuncionario(Guid id);
    }
}
