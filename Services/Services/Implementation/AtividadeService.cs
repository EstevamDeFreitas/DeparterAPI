﻿using AutoMapper;
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
                DataEntrega = atividade.DataEntrega,
                DepartamentoId = atividade.DepartamentoId,
                StatusAtividade = StatusAtividade.Pendente
                
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

        public void CreateAtividadeCheck(AtividadeCheckCreateDTO atividadeCheck, Guid funcionarioId)
        {
            HasAccess(funcionarioId, atividadeCheck.AtividadeId, NivelAcesso.Editar);

            var atividadeCheckCreate = _mapper.Map<AtividadeCheck>(atividadeCheck);

            atividadeCheckCreate.Gerar();

            _repository.AtividadeCheckRepository.Create(atividadeCheckCreate);
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


            if(atividadeFuncionario.NivelAcesso == NivelAcesso.Todos)
            {
                throw new CriadorNaoPodeSerRemovido();
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

        public void DeleteAtividadeCheck(Guid atividadeCheckId, Guid funcionarioId)
        {
            var atividadeCheck = _repository.AtividadeCheckRepository.FindById(atividadeCheckId).FirstOrDefault();

            HasAccess(funcionarioId, atividadeCheck.AtividadeId, NivelAcesso.Editar);

            _repository.AtividadeCheckRepository.Delete(atividadeCheck);
            _repository.Save();
        }

        public AtividadeDTO GetAtividade(Guid id, Guid funcionarioId)
        {
            UpdateDatabaseAtividadesStatus();

            HasAccess(funcionarioId, id, NivelAcesso.Ler);

            var atividadeFull = _repository.AtividadeRepository.FindFullById(id).FirstOrDefault();

            var atividadeFullDTO = _mapper.Map<AtividadeDTO>(atividadeFull);

            atividadeFullDTO.AtividadeFuncionarios.ForEach(funcionario =>
            {
                funcionario.FuncionarioEmail = atividadeFull.AtividadeFuncionarios.FirstOrDefault(x => x.FuncionarioId == funcionario.FuncionarioId).Funcionario.Email;
            });

            return atividadeFullDTO;
        }

        public AtividadeDTO GetAtividade(Guid id)
        {
            UpdateDatabaseAtividadesStatus();

            var atividadeFull = _repository.AtividadeRepository.FindFullById(id).FirstOrDefault();

            var atividadeFullDTO = _mapper.Map<AtividadeDTO>(atividadeFull);

            atividadeFullDTO.AtividadeFuncionarios.ForEach(funcionario =>
            {
                funcionario.FuncionarioEmail = atividadeFull.AtividadeFuncionarios.FirstOrDefault(x => x.FuncionarioId == funcionario.FuncionarioId).Funcionario.Email;
            });

            return atividadeFullDTO;
        }

        public AtividadeDTO GetAtividadeByScreenId(int screenId)
        {
            var atividade = _repository.AtividadeRepository.FindByCondition(x => x.OnScreenId == screenId).FirstOrDefault();

            if(atividade is null)
            {
                throw new EntidadeNaoEncontrada("Atividade");
            }

            return _mapper.Map<AtividadeDTO>(atividade);
        }

        public List<AtividadeDTO> GetAtividades(bool? isAdminSearch, Guid funcionarioId)
        {
            UpdateDatabaseAtividadesStatus();

            var atividades = _repository.AtividadeRepository.FindAllFull(isAdminSearch, funcionarioId).ToList();

            return _mapper.Map<List<AtividadeDTO>>(atividades);
        }

        public List<AtividadeDTO> GetAtividadesFuncionario(Guid funcionarioId)
        {
            UpdateDatabaseAtividadesStatus();

            var atividades = _repository.AtividadeRepository.FindByCondition(x => x.AtividadeFuncionarios.Any(x => x.FuncionarioId == funcionarioId)).ToList();

            return _mapper.Map<List<AtividadeDTO>>(atividades);
        }

        public ResumoAtividades GetResumoAtividades(TempoBusca tempoBusca, Guid? funcionarioId, Guid? departamentoId)
        {
            var tempo = Tempo.GetMaxTempo(tempoBusca);

            var atividades = _repository.AtividadeRepository.FindByCondition(x => (tempo.HasValue? x.DataCriacao <= tempo : true) && (funcionarioId.HasValue ? x.AtividadeFuncionarios.Any(y => y.FuncionarioId == funcionarioId) : true) && (departamentoId.HasValue ? departamentoId == x.DepartamentoId : true));

            var resumo = new ResumoAtividades
            {
                Atrasadas = atividades.Count(x => x.StatusAtividade == StatusAtividade.Atrasada),
                Finalizadas = atividades.Count(x => x.StatusAtividade == StatusAtividade.Conclúida),
                Pendente = atividades.Count(x => x.StatusAtividade == StatusAtividade.Pendente),
                EmDesenvolvimento = atividades.Count(x => x.StatusAtividade == StatusAtividade.Desenvolvendo)
            };

            return resumo;
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

            if(atividadeAcessoFuncionario.NivelAcesso == NivelAcesso.Todos)
            {
                atividadeAcessoFuncionario.NivelAcesso = NivelAcesso.Compartilhar;
            }

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

        public void UpdateAtividade(AtividadePutDTO atividade, Guid funcionarioId)
        {
            HasAccess(funcionarioId, atividade.Id.GetValueOrDefault(), NivelAcesso.Editar);

            var atividadeUpdated = _repository.AtividadeRepository.FindFullById(atividade.Id.GetValueOrDefault()).FirstOrDefault();

            if(atividadeUpdated is null)
            {
                throw new EntidadeNaoEncontrada("Atividade");
            }

            atividadeUpdated.Titulo = atividade.Titulo;
            atividadeUpdated.DataEntrega = atividade.DataEntrega;
            atividadeUpdated.Descricao = atividade.Descricao;
            atividadeUpdated.TempoPrevisto = atividade.TempoPrevisto;
            atividadeUpdated.AtividadePaiId = atividade.AtividadePaiId;
            atividadeUpdated.StatusAtividade = atividade.StatusAtividade;

            
            var newCategories = atividade.AtividadeCategorias.Where(x => !atividadeUpdated.AtividadeCategorias.Any(y => y.AtividadeId == x.AtividadeId && y.CategoriaId == x.CategoriaId));

            if (newCategories.Any())
            {
                _repository.AtividadeCategoriaRepository.CreateMultiple(_mapper.Map<List<AtividadeCategoria>>(newCategories));
                _repository.Save();
            }

            //Adicionar validações para categorias removidas

            var removeCategories = atividadeUpdated.AtividadeCategorias.Where(ac => !atividade.AtividadeCategorias.Any(acn => acn.AtividadeId == ac.AtividadeId && acn.CategoriaId == ac.CategoriaId));

            if (removeCategories.Any())
            {
                _repository.AtividadeCategoriaRepository.DeleteMultiple(removeCategories.ToList());
            }

            
            var newFuncionarios = atividade.AtividadeFuncionarios.Where(x => !atividadeUpdated.AtividadeFuncionarios.Any(y => y.AtividadeId == x.AtividadeId && y.FuncionarioId == x.FuncionarioId));

            if (newFuncionarios.Any())
            {
                newFuncionarios = newFuncionarios.Select(x => new AtividadeFuncionarioDTO
                {
                    AtividadeId = x.AtividadeId,
                    FuncionarioEmail = x.FuncionarioEmail,
                    FuncionarioId = x.FuncionarioId,
                    NivelAcesso = (x.NivelAcesso == NivelAcesso.Todos ? NivelAcesso.Compartilhar : x.NivelAcesso)
                });

                _repository.AtividadeFuncionarioRepository.CreateMultiple(_mapper.Map<List<AtividadeFuncionario>>(newFuncionarios));
            }
            //Adicionar validações para funcionarios removidos
            var removedFuncionarios = atividadeUpdated.AtividadeFuncionarios.Where(x => !atividade.AtividadeFuncionarios.Any(y => y.AtividadeId == x.AtividadeId && y.FuncionarioId == x.FuncionarioId));

            removedFuncionarios = removedFuncionarios.Where(x => x.NivelAcesso != NivelAcesso.Todos);

            if (removedFuncionarios.Any())
            {
                _repository.AtividadeFuncionarioRepository.DeleteMultiple(removedFuncionarios.ToList());
            }

            //Realizar update noq sobrar
            var funcionariosUpdated = atividadeUpdated.AtividadeFuncionarios.Where(x => atividade.AtividadeFuncionarios.Any(y => y.AtividadeId == x.AtividadeId && y.FuncionarioId == x.FuncionarioId)).ToList();

            funcionariosUpdated = funcionariosUpdated.Where(x => x.NivelAcesso != NivelAcesso.Todos).ToList();

            funcionariosUpdated.ForEach(func =>
            {
                func.NivelAcesso = atividade.AtividadeFuncionarios.FirstOrDefault(x => x.AtividadeId == func.AtividadeId && x.FuncionarioId == func.FuncionarioId).NivelAcesso;
                if(func.NivelAcesso == NivelAcesso.Todos)
                {
                    func.NivelAcesso = NivelAcesso.Compartilhar;
                }
            });

            _repository.AtividadeFuncionarioRepository.UpdateMultiple(funcionariosUpdated);

            _repository.AtividadeRepository.Update(atividadeUpdated);
            _repository.Save();
        }

        public void UpdateAtividadeCheck(AtividadeCheckDTO atividade, Guid funcionarioId)
        {
            var atividadeCheckUpdate = _repository.AtividadeCheckRepository.FindById(atividade.Id).FirstOrDefault();

            HasAccess(funcionarioId, atividadeCheckUpdate.AtividadeId, NivelAcesso.Editar);

            atividadeCheckUpdate.Checked = atividade.Checked;
            atividadeCheckUpdate.Descricao = atividade.Descricao;

            _repository.AtividadeCheckRepository.Update(atividadeCheckUpdate);
            _repository.Save();
        }

        public void UpdateDatabaseAtividadesStatus()
        {
            _repository.AtividadeRepository.UpdateDatabaseAtividadesStatus();
        }
    }
}
