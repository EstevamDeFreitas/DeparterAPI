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
    public class EquipeService : ServiceBase, IEquipeService
    {
        public EquipeService(IRepositoryWrapper repository, IMapper mapper, IServiceWrapper serviceWrapper) : base(repository, mapper, serviceWrapper)
        {
        }

        public void AddUsuarioEquipe(Guid equipeId, List<Guid> usuarioId)
        {
            var usuarioEquipe = usuarioId.Select(x => new EquipeUsuario
            {
                EquipeId = equipeId,
                UsuarioId = x
            }).ToList();

            _repository.EquipeUsuarioRepository.CreateMultiple(usuarioEquipe);
            _repository.Save();
        }

        public void CreateEquipe(EquipeCreateDTO equipe, Guid usuarioId)
        {
            var equipeCreate = _mapper.Map<Equipe>(equipe);

            equipeCreate.Gerar();

            if (!equipeCreate.EquipeUsuarios.Any(x => x.UsuarioId == usuarioId))
            {
                equipeCreate.EquipeUsuarios.Add(new EquipeUsuario
                {
                    UsuarioId = usuarioId,
                    EquipeId = equipeCreate.Id
                });
            }

            equipeCreate.EquipeUsuarios = equipeCreate.EquipeUsuarios.Select(x => new EquipeUsuario
            {
                EquipeId = equipeCreate.Id,
                UsuarioId = x.UsuarioId,
            }).ToList();

            _repository.EquipeRepository.Create(equipeCreate);
            _repository.Save();
        }

        public void DeleteEquipe(Guid equipeId)
        {
            _repository.EquipeRepository.DeleteById(equipeId);
            _repository.Save();
        }

        public EquipeDTO GetEquipe(Guid equipeId)
        {
            var equipe = _repository.EquipeRepository.FindById(equipeId).FirstOrDefault();

            if(equipe is null)
            {
                throw new EntidadeNaoEncontrada("Equipe");
            }

            equipe.Atividades = _repository.AtividadeRepository.FindByCondition(x => x.EquipeId == equipeId).ToList();
            equipe.EquipeUsuarios = _repository.EquipeUsuarioRepository.FindByCondition(x => x.EquipeId == equipe.Id).ToList();

            var response = _mapper.Map<EquipeDTO>(equipe);

            response.EquipeUsuarios.ForEach(fun =>
            {
                var funcTemp = _repository.UsuarioRepository.FindByCondition(x => x.Id == fun.UsuarioId).FirstOrDefault();

                fun.Usuario = new UsuarioDTO
                {
                    Nome = funcTemp.Nome,
                    Email = funcTemp.Email,
                    Imagem = funcTemp.Imagem,
                    Id = funcTemp.Id
                };
            });

            

            return response;
        }

        public EquipeDTO GetEquipe(Guid equipeId, Guid usuarioId)
        {
            HasAccess(usuarioId, equipeId);

            var equipe = _repository.EquipeRepository.FindById(equipeId).FirstOrDefault();

            if (equipe is null)
            {
                throw new EntidadeNaoEncontrada("Equipe");
            }

            equipe.Atividades = _repository.AtividadeRepository.FindByCondition(x => x.EquipeId == equipeId && x.AtividadeUsuarios.Any(y => y.UsuarioId == usuarioId)).ToList();
            equipe.EquipeUsuarios = _repository.EquipeUsuarioRepository.FindByCondition(x => x.EquipeId == equipe.Id).ToList();

            var response = _mapper.Map<EquipeDTO>(equipe);

            response.EquipeUsuarios.ForEach(fun =>
            {
                var funcTemp = _repository.UsuarioRepository.FindByCondition(x => x.Id == fun.UsuarioId).FirstOrDefault();

                fun.Usuario = new UsuarioDTO
                {
                    Nome = funcTemp.Nome,
                    Email = funcTemp.Email,
                    Imagem = funcTemp.Imagem,
                    Id = funcTemp.Id
                };
            });

            return response;
        }

        public List<EquipeAtividadesResumoDTO> GetEquipeAtividadesResumo(Guid equipeId)
        {
            var equipe = GetEquipe(equipeId);

            var response = equipe.Atividades.Select(x => new EquipeAtividadesResumoDTO
            {
                DataEntrega = x.DataEntrega,
                Descricao = x.Titulo,
                AtividadeId = x.Id.GetValueOrDefault(),
                Usuario = _serviceWrapper.UsuarioService.GetUsuario(_serviceWrapper.AtividadeService.GetAtividade(x.Id.GetValueOrDefault()).AtividadeUsuarios.FirstOrDefault(x => x.NivelAcesso == NivelAcesso.Todos).UsuarioId).Nome,
                Status = (x.DataEntrega < DateTime.Now && x.AtividadeChecks.Any(y => !y.Checked) ? "Em andamento" : (x.AtividadeChecks.Any(y => !y.Checked) ? "Atrasado" : "Finalizado"))
            }).OrderBy(x => x.DataEntrega).ToList();

            return response;
        }

        public List<EquipeDTO> GetEquipeList(bool? isAdminSearch, Guid usuarioId)
        {
            var equipes = _repository.EquipeRepository.FindByCondition(x => (isAdminSearch.HasValue && isAdminSearch == true) ? true : x.EquipeUsuarios.Any(y => y.UsuarioId == usuarioId));

            return _mapper.Map<List<EquipeDTO>>(equipes);
        }

        public void RemoveUsuarioEquipe(Guid equipeId, List<Guid> usuarioId, Guid usuarioLogadoId)
        {
            var usuarioEquipe = _repository.EquipeUsuarioRepository.FindByCondition(x => x.EquipeId == equipeId && usuarioId.Any(y => y == x.UsuarioId)).ToList();

            _repository.EquipeUsuarioRepository.DeleteMultiple(usuarioEquipe);
            _repository.Save();
        }

        public void UpdateEquipe(EquipeDTO equipe)
        {
            var equipeUpdate = _repository.EquipeRepository.FindById(equipe.Id).FirstOrDefault();

            equipeUpdate.Nome = equipe.Nome;
            equipeUpdate.Descricao = equipe.Descricao;
            equipeUpdate.MaximoHorasDiarias = equipe.MaximoHorasDiarias;
            equipeUpdate.MaximoHorasMensais = equipe.MaximoHorasMensais;
            equipeUpdate.ImageUrl = equipe.ImageUrl;

            _repository.EquipeRepository.Update(equipeUpdate);
            _repository.Save();
        }

        public void HasAccess(Guid usuarioId, Guid equipeId)
        {
            var equipeUsuario = _repository.EquipeUsuarioRepository.FindByCondition(x => x.UsuarioId == usuarioId && x.EquipeId == equipeId).FirstOrDefault();

            if (equipeUsuario is null)
            {
                throw new SemAutorizacao();
            }
        }

        public EquipeDTO GetEquipeByScreenId(int screenId)
        {
            var equipe = _repository.EquipeRepository.FindByCondition(x => x.OnScreenId == screenId).FirstOrDefault();

            if (equipe is null)
            {
                throw new EntidadeNaoEncontrada("Equipe");
            }

            return _mapper.Map<EquipeDTO>(equipe);
        }
    }
}
