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
    public class DepartamentoService : ServiceBase, IDepartamentoService
    {
        public DepartamentoService(IRepositoryWrapper repository, IMapper mapper, IServiceWrapper serviceWrapper) : base(repository, mapper, serviceWrapper)
        {
        }

        public void AddFuncionarioDepartamento(Guid departamentoId, List<Guid> funcionarioId)
        {
            var funcionarioDepartamento = funcionarioId.Select(x => new DepartamentoFuncionario
            {
                DepartamentoId = departamentoId,
                FuncionarioId = x
            }).ToList();

            _repository.DepartamentoFuncionarioRepository.CreateMultiple(funcionarioDepartamento);
            _repository.Save();
        }

        public void CreateDepartamento(DepartamentoCreateDTO departamento, Guid funcionarioId)
        {
            var departamentoCreate = _mapper.Map<Departamento>(departamento);

            departamentoCreate.Gerar();

            if (!departamentoCreate.DepartamentoFuncionarios.Any(x => x.FuncionarioId == funcionarioId))
            {
                departamentoCreate.DepartamentoFuncionarios.Add(new DepartamentoFuncionario
                {
                    FuncionarioId = funcionarioId,
                    DepartamentoId = departamentoCreate.Id
                });
            }

            departamentoCreate.DepartamentoFuncionarios = departamentoCreate.DepartamentoFuncionarios.Select(x => new DepartamentoFuncionario
            {
                DepartamentoId = departamentoCreate.Id,
                FuncionarioId = x.FuncionarioId,
            }).ToList();

            _repository.DepartamentoRepository.Create(departamentoCreate);
            _repository.Save();
        }

        public void DeleteDepartamento(Guid departamentoId)
        {
            _repository.DepartamentoRepository.DeleteById(departamentoId);
            _repository.Save();
        }

        public DepartamentoDTO GetDepartamento(Guid departamentoId)
        {
            var departamento = _repository.DepartamentoRepository.FindById(departamentoId).FirstOrDefault();

            if(departamento is null)
            {
                throw new EntidadeNaoEncontrada("Departamento");
            }

            departamento.Atividades = _repository.AtividadeRepository.FindByCondition(x => x.DepartamentoId == departamentoId).ToList();
            departamento.DepartamentoFuncionarios = _repository.DepartamentoFuncionarioRepository.FindByCondition(x => x.DepartamentoId == departamento.Id).ToList();

            var response = _mapper.Map<DepartamentoDTO>(departamento);

            response.DepartamentoFuncionarios.ForEach(fun =>
            {
                var funcTemp = _repository.FuncionarioRepository.FindByCondition(x => x.Id == fun.FuncionarioId).FirstOrDefault();

                fun.Funcionario = new FuncionarioDTO
                {
                    Nome = funcTemp.Nome,
                    Email = funcTemp.Email,
                    Imagem = funcTemp.Imagem,
                    Id = funcTemp.Id,
                    Apelido = funcTemp.Apelido
                };
            });

            

            return response;
        }

        public DepartamentoDTO GetDepartamento(Guid departamentoId, Guid funcionarioId)
        {
            HasAccess(funcionarioId, departamentoId);

            return GetDepartamento(departamentoId);
        }

        public List<DepartamentoAtividadesResumoDTO> GetDepartamentoAtividadesResumo(Guid departamentoId)
        {
            var departamento = GetDepartamento(departamentoId);

            var response = departamento.Atividades.Select(x => new DepartamentoAtividadesResumoDTO
            {
                DataEntrega = x.DataEntrega,
                Descricao = x.Titulo,
                AtividadeId = x.Id.GetValueOrDefault(),
                Funcionario = _serviceWrapper.FuncionarioService.GetFuncionario(_serviceWrapper.AtividadeService.GetAtividade(x.Id.GetValueOrDefault()).AtividadeFuncionarios.FirstOrDefault(x => x.NivelAcesso == NivelAcesso.Todos).FuncionarioId).Nome,
                Status = (x.DataEntrega < DateTime.Now && x.AtividadeChecks.Any(y => !y.Checked) ? "Em andamento" : (x.AtividadeChecks.Any(y => !y.Checked) ? "Atrasado" : "Finalizado"))
            }).OrderBy(x => x.DataEntrega).ToList();

            return response;
        }

        public List<DepartamentoDTO> GetDepartamentoList(bool? isAdminSearch, Guid funcionarioId)
        {
            var departamentos = _repository.DepartamentoRepository.FindByCondition(x => (isAdminSearch.HasValue && isAdminSearch == true) ? true : x.DepartamentoFuncionarios.Any(y => y.FuncionarioId == funcionarioId));

            return _mapper.Map<List<DepartamentoDTO>>(departamentos);
        }

        public void RemoveFuncionarioDepartamento(Guid departamentoId, List<Guid> funcionarioId, Guid funcionarioLogadoId)
        {
            var funcionarioDepartamento = _repository.DepartamentoFuncionarioRepository.FindByCondition(x => x.DepartamentoId == departamentoId && funcionarioId.Any(y => y == x.FuncionarioId)).ToList();

            _repository.DepartamentoFuncionarioRepository.DeleteMultiple(funcionarioDepartamento);
            _repository.Save();
        }

        public void UpdateDepartamento(DepartamentoDTO departamento)
        {
            var departamentoUpdate = _repository.DepartamentoRepository.FindById(departamento.Id).FirstOrDefault();

            departamentoUpdate.Nome = departamento.Nome;
            departamentoUpdate.Descricao = departamento.Descricao;
            departamentoUpdate.MaximoHorasDiarias = departamento.MaximoHorasDiarias;
            departamentoUpdate.MaximoHorasMensais = departamento.MaximoHorasMensais;

            _repository.DepartamentoRepository.Update(departamentoUpdate);
            _repository.Save();
        }

        public void HasAccess(Guid funcionarioId, Guid departamentoId)
        {
            var departamentoFuncionario = _repository.DepartamentoFuncionarioRepository.FindByCondition(x => x.FuncionarioId == funcionarioId && x.FuncionarioId == departamentoId).FirstOrDefault();

            if (departamentoFuncionario is null)
            {
                throw new SemAutorizacao();
            }
        }
    }
}
