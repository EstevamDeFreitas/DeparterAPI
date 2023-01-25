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
    public class DepartamentoService : ServiceBase, IDepartamentoService
    {
        public DepartamentoService(IRepositoryWrapper repository, IMapper mapper, IServiceWrapper serviceWrapper) : base(repository, mapper, serviceWrapper)
        {
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

        

        public DepartamentoDTO GetDepartamento(Guid departamentoId)
        {
            var departamento = _repository.DepartamentoRepository.FindById(departamentoId).FirstOrDefault();

            departamento.DepartamentoAtividades = _repository.DepartamentoAtividadeRepository.FindByCondition(x => x.DepartamentoId == departamento.Id).ToList();
            departamento.DepartamentoFuncionarios = _repository.DepartamentoFuncionarioRepository.FindByCondition(x => x.DepartamentoId == departamento.Id).ToList();

            var response = _mapper.Map<DepartamentoDTO>(departamento);

            response.DepartamentoFuncionarios.ForEach(fun =>
            {
                var funcTemp = _repository.FuncionarioRepository.FindByCondition(x => x.Id == fun.FuncionarioId).FirstOrDefault();

                fun.Funcionario = new FuncionarioDTO
                {
                    Nome = funcTemp.Nome,
                    Email = funcTemp.Email,
                    Imagem = funcTemp.Imagem
                };
            });

            return response;
        }

        public List<DepartamentoDTO> GetDepartamentoList()
        {
            var departamentos = _repository.DepartamentoRepository.GetAll();

            return _mapper.Map<List<DepartamentoDTO>>(departamentos);
        }
    }
}
