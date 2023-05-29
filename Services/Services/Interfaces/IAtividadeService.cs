using Domain.Entities;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface IAtividadeService
    {
        AtividadeDTO GetAtividade(Guid id, Guid funcionario);
        AtividadeDTO GetAtividade(Guid id);
        AtividadeDTO GetAtividadeByScreenId(int screenId);
        List<AtividadeDTO> GetAtividadesFuncionario(Guid funcionarioId);
        List<AtividadeDTO> GetAtividades(bool? isAdminSearch, Guid funcionarioId);
        ResumoAtividades GetResumoAtividades(TempoBusca tempoBusca, Guid? funcionarioId, Guid? departamentoId);
        void CreateAtividade(AtividadeCreateDTO atividade, Guid funcionarioId);
        void UpdateAtividade(AtividadePutDTO atividade, Guid funcionarioId);
        void DeleteAtividade(Guid id, Guid funcionarioId);
        void UpdateAccessAtividade(AtividadeAcessoFuncionario atividadeFuncionario, Guid funcionarioId);
        void DeleteAccessAtividade(AtividadeAcessoFuncionario atividadeFuncionario, Guid funcionarioId);
        void HasAccess(Guid funcionarioId, Guid atividadeId, NivelAcesso nivelAcesso);
        void CreateAtividadeCheck(AtividadeCheckCreateDTO atividadeCheck, Guid funcionarioId);
        void UpdateAtividadeCheck(AtividadeCheckDTO atividade, Guid funcionarioId);
        void DeleteAtividadeCheck(Guid atividadeCheckId, Guid funcionarioId);
        void UpdateDatabaseAtividadesStatus();
    }
}
