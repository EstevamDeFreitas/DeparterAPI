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

        public void CreateAtividade(AtividadeCreateDTO atividade, Guid usuarioId)
        {
            var atividadeCriar = new Atividade
            {
                AtividadePaiId = atividade.AtividadePaiId,
                Descricao = atividade.Descricao,
                TempoPrevisto = atividade.TempoPrevisto,
                Titulo = atividade.Titulo,
                DataEntrega = atividade.DataEntrega,
                EquipeId = atividade.EquipeId,
                StatusAtividade = StatusAtividade.Pendente
                
            };

            atividadeCriar.Gerar();

            atividadeCriar.AtividadeCategorias = atividade.Categorias.Select(x => new AtividadeCategoria { AtividadeId = atividadeCriar.Id, CategoriaId = x }).ToList();

            var usuarios = _repository.UsuarioRepository.GetUsuariosFromEmails(atividade.AtividadeUsuarios.Select(x => x.UsuarioEmail).ToList()).ToList();

            if(!usuarios.Any(x => x.Id == usuarioId))
            {
                var usuarioCriador = _repository.UsuarioRepository.FindById(usuarioId).FirstOrDefault();

                usuarios.Add(usuarioCriador);
                atividade.AtividadeUsuarios.Add(new AtividadeUsuarioCreateDTO { UsuarioEmail = usuarioCriador.Email, NivelAcesso = NivelAcesso.Todos });
            }

            atividadeCriar.AtividadeUsuarios = atividade.AtividadeUsuarios.Select(x => new AtividadeUsuario
            {
                AtividadeId = atividadeCriar.Id,
                UsuarioId = usuarios.Find(y => y.Email == x.UsuarioEmail).Id,
                NivelAcesso = usuarios.Find(y => y.Email == x.UsuarioEmail).Id == usuarioId? NivelAcesso.Todos : x.NivelAcesso
            }).ToList();

            _repository.AtividadeRepository.Create(atividadeCriar);
            _repository.Save();

        }

        public void CreateAtividadeCheck(AtividadeCheckCreateDTO atividadeCheck, Guid usuarioId)
        {
            HasAccess(usuarioId, atividadeCheck.AtividadeId, NivelAcesso.Editar);

            var atividadeCheckCreate = _mapper.Map<AtividadeCheck>(atividadeCheck);

            atividadeCheckCreate.Gerar();

            _repository.AtividadeCheckRepository.Create(atividadeCheckCreate);
            _repository.Save();
        }

        public void DeleteAccessAtividade(AtividadeAcessoUsuario atividadeAcessoUsuario, Guid usuarioId)
        {
            HasAccess(usuarioId, atividadeAcessoUsuario.AtividadeId, NivelAcesso.Compartilhar);

            var atividadeUsuario = _repository.AtividadeUsuarioRepository.FindByCondition(x => x.AtividadeId == atividadeAcessoUsuario.AtividadeId && x.UsuarioId == atividadeAcessoUsuario.UsuarioId).FirstOrDefault();

            if(atividadeUsuario is null)
            {
                throw new EntidadeNaoEncontrada("Atividade Usuario");
            }


            if(atividadeUsuario.NivelAcesso == NivelAcesso.Todos)
            {
                throw new CriadorNaoPodeSerRemovido();
            }


            _repository.AtividadeUsuarioRepository.Delete(atividadeUsuario);
            _repository.Save();
        }

        public void DeleteAtividade(Guid id, Guid usuarioId)
        {
            HasAccess(usuarioId, id, NivelAcesso.Deletar);

            _repository.AtividadeRepository.DeleteById(id);
            _repository.Save();
        }

        public void DeleteAtividadeCheck(Guid atividadeCheckId, Guid usuarioId)
        {
            var atividadeCheck = _repository.AtividadeCheckRepository.FindById(atividadeCheckId).FirstOrDefault();

            HasAccess(usuarioId, atividadeCheck.AtividadeId, NivelAcesso.Editar);

            _repository.AtividadeCheckRepository.Delete(atividadeCheck);
            _repository.Save();
        }

        public AtividadeDTO GetAtividade(Guid id, Guid usuarioId)
        {
            UpdateDatabaseAtividadesStatus();

            HasAccess(usuarioId, id, NivelAcesso.Ler);

            var atividadeFull = _repository.AtividadeRepository.FindFullById(id).FirstOrDefault();

            var atividadeFullDTO = _mapper.Map<AtividadeDTO>(atividadeFull);

            atividadeFullDTO.AtividadeUsuarios.ForEach(usuario =>
            {
                usuario.UsuarioEmail = atividadeFull.AtividadeUsuarios.FirstOrDefault(x => x.UsuarioId == usuario.UsuarioId).Usuario.Email;
            });

            return atividadeFullDTO;
        }

        public AtividadeDTO GetAtividade(Guid id)
        {
            UpdateDatabaseAtividadesStatus();

            var atividadeFull = _repository.AtividadeRepository.FindFullById(id).FirstOrDefault();

            var atividadeFullDTO = _mapper.Map<AtividadeDTO>(atividadeFull);

            atividadeFullDTO.AtividadeUsuarios.ForEach(usuario =>
            {
                usuario.UsuarioEmail = atividadeFull.AtividadeUsuarios.FirstOrDefault(x => x.UsuarioId == usuario.UsuarioId).Usuario.Email;
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

        public List<AtividadeDTO> GetAtividades(bool? isAdminSearch, Guid usuarioId)
        {
            UpdateDatabaseAtividadesStatus();

            var atividades = _repository.AtividadeRepository.FindAllFull(isAdminSearch, usuarioId).ToList();

            return _mapper.Map<List<AtividadeDTO>>(atividades);
        }

        public List<AtividadeDTO> GetAtividadesUsuario(Guid usuarioId)
        {
            UpdateDatabaseAtividadesStatus();

            var atividades = _repository.AtividadeRepository.FindByCondition(x => x.AtividadeUsuarios.Any(x => x.UsuarioId == usuarioId)).ToList();

            return _mapper.Map<List<AtividadeDTO>>(atividades);
        }

        public ResumoAtividades GetResumoAtividades(TempoBusca tempoBusca, Guid? usuarioId, Guid? equipeId)
        {
            var tempo = Tempo.GetMaxTempo(tempoBusca);

            var atividades = _repository.AtividadeRepository.FindByCondition(x => (tempo.HasValue? x.DataCriacao <= tempo : true) && (usuarioId.HasValue ? x.AtividadeUsuarios.Any(y => y.UsuarioId == usuarioId) : true) && (equipeId.HasValue ? equipeId == x.EquipeId : true));

            var resumo = new ResumoAtividades
            {
                Atrasadas = atividades.Count(x => x.StatusAtividade == StatusAtividade.Atrasada),
                Finalizadas = atividades.Count(x => x.StatusAtividade == StatusAtividade.Conclúida),
                Pendente = atividades.Count(x => x.StatusAtividade == StatusAtividade.Pendente),
                EmDesenvolvimento = atividades.Count(x => x.StatusAtividade == StatusAtividade.Desenvolvendo)
            };

            return resumo;
        }

        public void HasAccess(Guid usuarioId, Guid atividadeId, NivelAcesso nivelAcesso)
        {
            var atividadeUsuario = _repository.AtividadeUsuarioRepository.FindByCondition(x => x.UsuarioId == usuarioId && x.AtividadeId == atividadeId).FirstOrDefault();

            if(atividadeUsuario is null || atividadeUsuario.NivelAcesso < nivelAcesso)
            {
                throw new SemAutorizacao();
            }
        }

        public void UpdateAccessAtividade(AtividadeAcessoUsuario atividadeAcessoUsuario, Guid usuarioId)
        {
            HasAccess(usuarioId, atividadeAcessoUsuario.AtividadeId, NivelAcesso.Compartilhar);

            var existsAtividadeUsuario = _repository.AtividadeUsuarioRepository.FindByCondition(x => x.AtividadeId == atividadeAcessoUsuario.AtividadeId && x.UsuarioId == atividadeAcessoUsuario.UsuarioId).FirstOrDefault();

            if(atividadeAcessoUsuario.NivelAcesso == NivelAcesso.Todos)
            {
                atividadeAcessoUsuario.NivelAcesso = NivelAcesso.Compartilhar;
            }

            if(existsAtividadeUsuario is null)
            {
                var atividadeUsuario = new AtividadeUsuario
                {
                    AtividadeId = atividadeAcessoUsuario.AtividadeId,
                    UsuarioId = atividadeAcessoUsuario.UsuarioId,
                    NivelAcesso = atividadeAcessoUsuario.NivelAcesso
                };

                _repository.AtividadeUsuarioRepository.Create(atividadeUsuario);
                _repository.Save();

                return;
            }

            existsAtividadeUsuario.NivelAcesso = atividadeAcessoUsuario.NivelAcesso;

            _repository.AtividadeUsuarioRepository.Update(existsAtividadeUsuario);
            _repository.Save();
        }

        public void UpdateAtividade(AtividadePutDTO atividade, Guid usuarioId)
        {
            HasAccess(usuarioId, atividade.Id.GetValueOrDefault(), NivelAcesso.Editar);

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

            
            var newUsuarios = atividade.AtividadeUsuarios.Where(x => !atividadeUpdated.AtividadeUsuarios.Any(y => y.AtividadeId == x.AtividadeId && y.UsuarioId == x.UsuarioId));

            if (newUsuarios.Any())
            {
                newUsuarios = newUsuarios.Select(x => new AtividadeUsuarioDTO
                {
                    AtividadeId = x.AtividadeId,
                    UsuarioEmail = x.UsuarioEmail,
                    UsuarioId = x.UsuarioId,
                    NivelAcesso = (x.NivelAcesso == NivelAcesso.Todos ? NivelAcesso.Compartilhar : x.NivelAcesso)
                });

                _repository.AtividadeUsuarioRepository.CreateMultiple(_mapper.Map<List<AtividadeUsuario>>(newUsuarios));
            }
            //Adicionar validações para usuarios removidos
            var removedUsuarios = atividadeUpdated.AtividadeUsuarios.Where(x => !atividade.AtividadeUsuarios.Any(y => y.AtividadeId == x.AtividadeId && y.UsuarioId == x.UsuarioId));

            removedUsuarios = removedUsuarios.Where(x => x.NivelAcesso != NivelAcesso.Todos);

            if (removedUsuarios.Any())
            {
                _repository.AtividadeUsuarioRepository.DeleteMultiple(removedUsuarios.ToList());
            }

            //Realizar update noq sobrar
            var usuariosUpdated = atividadeUpdated.AtividadeUsuarios.Where(x => atividade.AtividadeUsuarios.Any(y => y.AtividadeId == x.AtividadeId && y.UsuarioId == x.UsuarioId)).ToList();

            usuariosUpdated = usuariosUpdated.Where(x => x.NivelAcesso != NivelAcesso.Todos).ToList();

            usuariosUpdated.ForEach(func =>
            {
                func.NivelAcesso = atividade.AtividadeUsuarios.FirstOrDefault(x => x.AtividadeId == func.AtividadeId && x.UsuarioId == func.UsuarioId).NivelAcesso;
                if(func.NivelAcesso == NivelAcesso.Todos)
                {
                    func.NivelAcesso = NivelAcesso.Compartilhar;
                }
            });

            _repository.AtividadeUsuarioRepository.UpdateMultiple(usuariosUpdated);

            _repository.AtividadeRepository.Update(atividadeUpdated);
            _repository.Save();
        }

        public void UpdateAtividadeCheck(AtividadeCheckDTO atividade, Guid usuarioId)
        {
            var atividadeCheckUpdate = _repository.AtividadeCheckRepository.FindById(atividade.Id).FirstOrDefault();

            HasAccess(usuarioId, atividadeCheckUpdate.AtividadeId, NivelAcesso.Editar);

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
