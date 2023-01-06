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
        AtividadeDTO GetAtividade(Guid id);
        List<AtividadeDTO> GetAtividadesFuncionario(Guid funcionarioId);
        List<AtividadeDTO> GetAtividades();
        void CreateAtividade(AtividadeCreateDTO atividade, Guid funcionarioId);
        void UpdateAtividade(AtividadeDTO atividade, Guid funcionarioId);
        void DeleteAtividade(Guid id, Guid funcionarioId);
        void UpdateAccessAtividade(AtividadeFuncionarioCreateDTO atividadeFuncionario);
        void DeleteAccessAtividade(string funcionarioEmail);
        void HasAccess(Guid funcionarioId, Guid atividadeId, NivelAcesso nivelAcesso);
    }
}
