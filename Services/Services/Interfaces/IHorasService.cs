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
        void CreateHoras(FuncionarioAtividadeHorasCreateDTO funcionarioAtividadeHoras);
        void UpdateHoras(FuncionarioAtividadeHorasUpdateDTO funcionarioAtividadeHoras);
        void DeleteHoras(Guid horaId);

        List<FuncionarioHorasConfiguracaoDTO> GetFuncionarioHorasConfiguracoes(Guid funcionarioId);
        void CreateFuncionarioHorasConfiguracao(FuncionarioHorasConfiguracaoCreateDTO funcionarioHorasConfiguracao);
        void UpdateFuncionarioHorasConfiguracao(FuncionarioHorasConfiguracaoUpdateDTO funcionarioHorasConfiguracao);
        void DeleteFuncionarioHorasConfiguracao(Guid horaConfiguracaoId);
    }
}
