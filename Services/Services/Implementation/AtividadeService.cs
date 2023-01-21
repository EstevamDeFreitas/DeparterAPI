using AutoMapper;
using Domain.Entities;
using Persistence.Repositories.Interfaces;
using Services.DTO;
using Services.Exceptions;
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
                NivelAcesso = funcionarios.Find(y => y.Email == x.FuncionarioEmail).Id == funcionarioId? NivelAcesso.Todos : x.NivelAcesso
            }).ToList();

            _repository.AtividadeRepository.Create(atividadeCriar);
            _repository.Save();

        }

        public void DeleteAccessAtividade(AtividadeAcessoFuncionario atividadeAcessoFuncionario, Guid funcionarioId)
        {
            HasAccess(funcionarioId, atividadeAcessoFuncionario.AtividadeId, NivelAcesso.Compartilhar);

            var atividadeFuncionario = _repository.AtividadeFuncionarioRepository.FindByCondition(x => x.AtividadeId == atividadeAcessoFuncionario.AtividadeId && x.FuncionarioId == atividadeAcessoFuncionario.FuncionarioId).FirstOrDefault();

            if(atividadeFuncionario is null)
            {
                throw new EntidadeNaoEncontrada("Atividade Funcionario");
            }

            _repository.AtividadeFuncionarioRepository.Delete(atividadeFuncionario);
            _repository.Save();
        }

        public void DeleteAtividade(Guid id, Guid funcionarioId)
        {
            HasAccess(funcionarioId, id, NivelAcesso.Deletar);

            _repository.AtividadeRepository.DeleteById(id);
            _repository.Save();
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
            var atividades = _repository.AtividadeRepository.FindAllFull().ToList();

            return _mapper.Map<List<AtividadeDTO>>(atividades);
        }

        public List<AtividadeDTO> GetAtividadesFuncionario(Guid funcionarioId)
        {
            throw new NotImplementedException();
        }

        public void HasAccess(Guid funcionarioId, Guid atividadeId, NivelAcesso nivelAcesso)
        {
            var atividadeFuncionario = _repository.AtividadeFuncionarioRepository.FindByCondition(x => x.FuncionarioId == funcionarioId && x.AtividadeId == atividadeId).FirstOrDefault();

            if(atividadeFuncionario is null || atividadeFuncionario.NivelAcesso < nivelAcesso)
            {
                throw new SemAutorizacao();
            }
        }

        public void UpdateAccessAtividade(AtividadeAcessoFuncionario atividadeAcessoFuncionario, Guid funcionarioId)
        {
            HasAccess(funcionarioId, atividadeAcessoFuncionario.AtividadeId, NivelAcesso.Compartilhar);

            var existsAtividadeFuncionario = _repository.AtividadeFuncionarioRepository.FindByCondition(x => x.AtividadeId == atividadeAcessoFuncionario.AtividadeId && x.FuncionarioId == atividadeAcessoFuncionario.FuncionarioId).FirstOrDefault();

            if(existsAtividadeFuncionario is null)
            {
                var atividadeFuncionario = new AtividadeFuncionario
                {
                    AtividadeId = atividadeAcessoFuncionario.AtividadeId,
                    FuncionarioId = atividadeAcessoFuncionario.FuncionarioId,
                    NivelAcesso = atividadeAcessoFuncionario.NivelAcesso
                };

                _repository.AtividadeFuncionarioRepository.Create(atividadeFuncionario);
                _repository.Save();

                return;
            }

            existsAtividadeFuncionario.NivelAcesso = atividadeAcessoFuncionario.NivelAcesso;

            _repository.AtividadeFuncionarioRepository.Update(existsAtividadeFuncionario);
            _repository.Save();
        }

        public void UpdateAtividade(AtividadeDTO atividade, Guid funcionarioId)
        {
            HasAccess(funcionarioId, atividade.Id.GetValueOrDefault(), NivelAcesso.Editar);

            var atividadeUpdated = _repository.AtividadeRepository.FindById(atividade.Id.GetValueOrDefault()).ToList().FirstOrDefault();

            if(atividadeUpdated is null)
            {
                throw new EntidadeNaoEncontrada("Atividade");
            }

            atividadeUpdated.Titulo = atividade.Titulo;
            atividadeUpdated.DataEntrega = atividade.DataEntrega;
            atividadeUpdated.Descricao = atividade.Descricao;
            atividadeUpdated.TempoPrevisto = atividade.TempoPrevisto;
            
            _repository.AtividadeRepository.Update(atividadeUpdated);
            _repository.Save();
        }
    }
}
