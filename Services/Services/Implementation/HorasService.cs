using Services.DTO;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Implementation
{
    public class HorasService : IHorasService
    {
        public void CreateFuncionarioHorasConfiguracao(FuncionarioHorasConfiguracaoDTO funcionarioHorasConfiguracao)
        {
            throw new NotImplementedException();
        }

        public void CreateHoras(FuncionarioAtividadeHorasDTO funcionarioAtividadeHoras)
        {
            throw new NotImplementedException();
        }

        public void DeleteFuncionarioHorasConfiguracao(Guid horaConfiguracaoId)
        {
            throw new NotImplementedException();
        }

        public void DeleteHoras(Guid horaId)
        {
            throw new NotImplementedException();
        }

        public List<FuncionarioAtividadeHorasDTO> GetAtividadeHoras(Guid atividadeId)
        {
            throw new NotImplementedException();
        }

        public List<FuncionarioAtividadeHorasDTO> GetFuncionarioHoras(Guid funcionarioId)
        {
            throw new NotImplementedException();
        }

        public List<FuncionarioHorasConfiguracaoDTO> GetFuncionarioHorasConfiguracoes(Guid funcionarioId)
        {
            throw new NotImplementedException();
        }

        public List<FuncionarioAtividadeHorasDTO> GetHoras()
        {
            throw new NotImplementedException();
        }

        public void UpdateFuncionarioHorasConfiguracao(FuncionarioHorasConfiguracaoDTO funcionarioHorasConfiguracao)
        {
            throw new NotImplementedException();
        }

        public void UpdateHoras(FuncionarioAtividadeHorasDTO funcionarioAtividadeHoras, Guid funcionarioId)
        {
            throw new NotImplementedException();
        }
    }
}
