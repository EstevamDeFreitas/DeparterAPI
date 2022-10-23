using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface ICategoriaService
    {
        CategoriaDTO GetCategoria(Guid id);
        List<CategoriaDTO> GetCategorias();
        void CreateCategoria(CategoriaDTO categoria);
        void UpdateCategoria(CategoriaDTO categoria);
        void DeleteCategoria(Guid id);
    }
}
