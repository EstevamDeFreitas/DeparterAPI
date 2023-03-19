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
        List<FuncionarioAtividadeHorasDTO> GetFuncionarioHoras(Guid funcionarioId);
        List<FuncionarioAtividadeHorasDTO> GetHoras();
        List<FuncionarioAtividadeHorasDTO> GetAtividadeHoras(Guid atividadeId);
        List<FuncionarioAtividadeHorasDTO> GetFuncionarioAtividadeHoras(Guid funcionarioId, Guid atividadeId);

        HorasResumo GetHorasResumo(Guid? funcionarioId, Guid? departamentoId);
        List<HorasCategoria> GetHorasCategorias(Guid? funcionarioId, Guid? departamentoId);

        void CreateHoras(FuncionarioAtividadeHorasCreateDTO funcionarioAtividadeHoras);
        void UpdateHoras(FuncionarioAtividadeHorasUpdateDTO funcionarioAtividadeHoras);
        void DeleteHoras(Guid horaId);

        List<FuncionarioHorasConfiguracaoDTO> GetFuncionarioHorasConfiguracoes(Guid funcionarioId);
        void CreateFuncionarioHorasConfiguracao(FuncionarioHorasConfiguracaoCreateDTO funcionarioHorasConfiguracao);
        void UpdateFuncionarioHorasConfiguracao(FuncionarioHorasConfiguracaoUpdateDTO funcionarioHorasConfiguracao);
        void DeleteFuncionarioHorasConfiguracao(Guid horaConfiguracaoId);
    }
}
