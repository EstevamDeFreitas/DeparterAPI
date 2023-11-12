using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface IUsuarioService
    {
        UsuarioDTO GetUsuario(Guid id);
        List<UsuarioDTO> GetUsuarios();
        void CreateUsuario(UsuarioDTO usuario);
        void UpdateUsuario(UsuarioDTO usuario);
        void DeleteUsuario(Guid id);
    }
}
