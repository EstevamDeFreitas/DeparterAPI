﻿using Services.DTO;
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
        List<DepartamentoDTO> GetDepartamentoList();
        DepartamentoDTO GetDepartamento(Guid departamentoId);
    }
}
