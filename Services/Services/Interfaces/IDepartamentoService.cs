using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface IDepartamentoService
    {
        void CreateDepartamento(DepartamentoCreateDTO departamento, Guid funcionarioId);
        List<DepartamentoDTO> GetDepartamentoList(bool? isAdminSearch, Guid funcionarioId);
        DepartamentoDTO GetDepartamento(Guid departamentoId);
        DepartamentoDTO GetDepartamento(Guid departamentoId, Guid funcionarioId);
        void DeleteDepartamento(Guid departamentoId);
        void UpdateDepartamento(DepartamentoDTO departamento);
        void AddFuncionarioDepartamento(Guid departamentoId, List<Guid> funcionarioId);
        void RemoveFuncionarioDepartamento(Guid departamentoId, List<Guid> funcionarioId, Guid funcionarioLogadoId);
        List<DepartamentoAtividadesResumoDTO> GetDepartamentoAtividadesResumo(Guid departamentoId);
    }
}
