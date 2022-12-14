using AutoMapper;
using Domain.Entities;
using Persistence.Repositories.Interfaces;
using Services.DTO;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Implementation
{
    public class AtividadeService : ServiceBase, IAtividadeService
    {
        public AtividadeService(IRepositoryWrapper repository, IMapper mapper, IServiceWrapper serviceWrapper) : base(repository, mapper, serviceWrapper)
        {
        }

        public void CreateAtividade(AtividadeCreateDTO atividade, Guid funcionarioId)
        {
            var atividadeCriar = new Atividade
            {
                AtividadePaiId = atividade.AtividadePaiId,
                Descricao = atividade.Descricao,
                TempoPrevisto = atividade.TempoPrevisto,
                Titulo = atividade.Titulo,
                DataEntrega = atividade.DataEntrega
            };

            atividadeCriar.Gerar();

            atividadeCriar.AtividadeCategorias = atividade.Categorias.Select(x => new AtividadeCategoria { AtividadeId = atividadeCriar.Id, CategoriaId = x }).ToList();

            var funcionarios = _repository.FuncionarioRepository.GetFuncionariosFromEmails(atividade.AtividadeFuncionarios.Select(x => x.FuncionarioEmail).ToList()).ToList();

            if(!funcionarios.Any(x => x.Id == funcionarioId))
            {
                var funcionarioCriador = _repository.FuncionarioRepository.FindById(funcionarioId).FirstOrDefault();

                funcionarios.Add(funcionarioCriador);
                atividade.AtividadeFuncionarios.Add(new AtividadeFuncionarioCreateDTO { FuncionarioEmail = funcionarioCriador.Email, NivelAcesso = NivelAcesso.Todos });
            }

            atividadeCriar.AtividadeFuncionarios = atividade.AtividadeFuncionarios.Select(x => new AtividadeFuncionario
            {
                AtividadeId = atividadeCriar.Id,
                FuncionarioId = funcionarios.Find(y => y.Email == x.FuncionarioEmail).Id,
                NivelAcesso = x.NivelAcesso
            }).ToList();

            _repository.AtividadeRepository.Create(atividadeCriar);
            _repository.Save();

        }

        public void DeleteAccessAtividade(string funcionarioEmail)
        {
            throw new NotImplementedException();
        }

        public void DeleteAtividade(Guid id)
        {
            throw new NotImplementedException();
        }

        public AtividadeDTO GetAtividade(Guid id)
        {
            var atividadeFull = _repository.AtividadeRepository.FindFullById(id).FirstOrDefault();

            var atividadeFullDTO = _mapper.Map<AtividadeDTO>(atividadeFull);

            atividadeFullDTO.AtividadeFuncionarios.ForEach(funcionario =>
            {
                funcionario.FuncionarioEmail = atividadeFull.AtividadeFuncionarios.FirstOrDefault(x => x.FuncionarioId == funcionario.FuncionarioId).Funcionario.Email;
            });

            return atividadeFullDTO;
        }

        public List<AtividadeDTO> GetAtividades()
        {
            var atividades = _repository.AtividadeRepository.GetAll().ToList();

            return _mapper.Map<List<AtividadeDTO>>(atividades);
        }

        public List<AtividadeDTO> GetAtividadesFuncionario(Guid funcionarioId)
        {
            throw new NotImplementedException();
        }

        public bool HasAccess(Guid funcionarioId, Guid atividadeId, NivelAcesso nivelAcesso)
        {
            var atividadeFuncionario = _repository.AtividadeFuncionarioRepository.FindByCondition(x => x.FuncionarioId == funcionarioId && x.AtividadeId == atividadeId).FirstOrDefault();

            if(atividadeFuncionario is null)
            {
                return false;
            }

            return atividadeFuncionario.NivelAcesso >= nivelAcesso;
        }

        public void UpdateAccessAtividade(AtividadeFuncionarioCreateDTO atividadeFuncionario)
        {
            throw new NotImplementedException();
        }

        public void UpdateAtividade(AtividadeDTO atividade)
        {
            
        }
    }
}
