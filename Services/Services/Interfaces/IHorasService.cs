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
        void CreateHoras(FuncionarioAtividadeHorasDTO funcionarioAtividadeHoras);
        void UpdateHoras(FuncionarioAtividadeHorasDTO funcionarioAtividadeHoras, Guid funcionarioId);
        void DeleteHoras(Guid horaId);

        List<FuncionarioHorasConfiguracaoDTO> GetFuncionarioHorasConfiguracoes(Guid funcionarioId);
        void CreateFuncionarioHorasConfiguracao(FuncionarioHorasConfiguracaoDTO funcionarioHorasConfiguracao);
        void UpdateFuncionarioHorasConfiguracao(FuncionarioHorasConfiguracaoDTO funcionarioHorasConfiguracao);
        void DeleteFuncionarioHorasConfiguracao(Guid horaConfiguracaoId);
    }
}
