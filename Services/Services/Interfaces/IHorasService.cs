using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface IHorasService
    {
        List<UsuarioAtividadeHorasDTO> GetUsuarioHoras(Guid usuarioId);
        List<UsuarioAtividadeHorasDTO> GetHoras();
        List<UsuarioAtividadeHorasDTO> GetAtividadeHoras(Guid atividadeId);
        List<UsuarioAtividadeHorasDTO> GetUsuarioAtividadeHoras(Guid usuarioId, Guid atividadeId);

        HorasResumo GetHorasResumo(Guid? usuarioId, Guid? equipeId);
        List<HorasCategoria> GetHorasCategorias(Guid? usuarioId, Guid? equipeId);

        void CreateHoras(UsuarioAtividadeHorasCreateDTO usuarioAtividadeHoras);
        void UpdateHoras(UsuarioAtividadeHorasUpdateDTO usuarioAtividadeHoras);
        void DeleteHoras(Guid horaId);

        List<UsuarioHorasConfiguracaoDTO> GetUsuarioHorasConfiguracoes(Guid usuarioId);
        void CreateUsuarioHorasConfiguracao(UsuarioHorasConfiguracaoCreateDTO usuarioHorasConfiguracao);
        void UpdateUsuarioHorasConfiguracao(UsuarioHorasConfiguracaoUpdateDTO usuarioHorasConfiguracao);
        void DeleteUsuarioHorasConfiguracao(Guid horaConfiguracaoId);
    }
}
