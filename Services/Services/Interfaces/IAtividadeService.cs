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
        AtividadeDTO GetAtividade(Guid id, Guid usuario);
        AtividadeDTO GetAtividade(Guid id);
        AtividadeDTO GetAtividadeByScreenId(int screenId);
        List<AtividadeDTO> GetAtividadesUsuario(Guid usuarioId);
        List<AtividadeDTO> GetAtividades(bool? isAdminSearch, Guid usuarioId);
        ResumoAtividades GetResumoAtividades(TempoBusca tempoBusca, Guid? usuarioId, Guid? equipeId);
        void CreateAtividade(AtividadeCreateDTO atividade, Guid usuarioId);
        void UpdateAtividade(AtividadePutDTO atividade, Guid usuarioId);
        void DeleteAtividade(Guid id, Guid usuarioId);
        void UpdateAccessAtividade(AtividadeAcessoUsuario atividadeUsuario, Guid usuarioId);
        void DeleteAccessAtividade(AtividadeAcessoUsuario atividadeUsuario, Guid usuarioId);
        void HasAccess(Guid usuarioId, Guid atividadeId, NivelAcesso nivelAcesso);
        void CreateAtividadeCheck(AtividadeCheckCreateDTO atividadeCheck, Guid usuarioId);
        void UpdateAtividadeCheck(AtividadeCheckDTO atividade, Guid usuarioId);
        void DeleteAtividadeCheck(Guid atividadeCheckId, Guid usuarioId);
        void UpdateDatabaseAtividadesStatus();
    }
}
