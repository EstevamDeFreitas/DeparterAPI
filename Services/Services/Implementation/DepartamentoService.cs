﻿using AutoMapper;
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

        public void AddFuncionarioDepartamento(Guid departamentoId, Guid funcionarioId)
        {
            var funcionarioDepartamento = new DepartamentoFuncionario
            {
                DepartamentoId = departamentoId,
                FuncionarioId = funcionarioId
            };

            _repository.DepartamentoFuncionarioRepository.Create(funcionarioDepartamento);
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

        public void RemoveFuncionarioDepartamento(Guid departamentoId, Guid funcionarioId)
        {
            var funcionarioDepartamento = _repository.DepartamentoFuncionarioRepository.FindByCondition(x => x.DepartamentoId == departamentoId && x.FuncionarioId == funcionarioId).FirstOrDefault();

            _repository.DepartamentoFuncionarioRepository.Delete(funcionarioDepartamento);
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
    }
}
