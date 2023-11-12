using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface IEquipeService
    {
        void CreateEquipe(EquipeCreateDTO equipe, Guid usuarioId);
        List<EquipeDTO> GetEquipeList(bool? isAdminSearch, Guid usuarioId);
        EquipeDTO GetEquipe(Guid equipeId);
        EquipeDTO GetEquipe(Guid equipeId, Guid usuarioId);
        EquipeDTO GetEquipeByScreenId(int screenId);
        void DeleteEquipe(Guid equipeId);
        void UpdateEquipe(EquipeDTO equipe);
        void AddUsuarioEquipe(Guid equipeId, List<Guid> usuarioId);
        void RemoveUsuarioEquipe(Guid equipeId, List<Guid> usuarioId, Guid usuarioLogadoId);
        List<EquipeAtividadesResumoDTO> GetEquipeAtividadesResumo(Guid equipeId);
    }
}
