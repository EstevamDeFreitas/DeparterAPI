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
        void CreateDepartamento(DepartamentoDTO departamento, Guid funcionarioId);
        List<DepartamentoDTO> GetDepartamentoList();
    }
}
