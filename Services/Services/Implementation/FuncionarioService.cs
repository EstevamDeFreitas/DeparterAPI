using AutoMapper;
using Domain.Entities;
using Persistence.Repositories.Interfaces;
using Services.DTO;
using Services.Exceptions;
using Services.Middleware;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Implementation
{
    public class FuncionarioService : ServiceBase, IFuncionarioService
    {
        public FuncionarioService(IRepositoryWrapper repository, IMapper mapper, IServiceWrapper serviceWrapper) : base(repository, mapper, serviceWrapper)
        {
        }

        public void CreateFuncionario(FuncionarioDTO funcionario)
        {
            var funcionarioFound = _repository.FuncionarioRepository.GetFuncionariosFromEmails(new List<string> { funcionario.Email }).FirstOrDefault();

            if(funcionarioFound is not null)
            {
                throw new EmailJaEstaEmUso(funcionario.Email);
            }

            var funcionarioCreate = _mapper.Map<Funcionario>(funcionario);

            funcionarioCreate.Gerar();

            _repository.FuncionarioRepository.Create(funcionarioCreate);
            _repository.Save();
        }

        public void DeleteFuncionario(Guid id)
        {
            _repository.FuncionarioRepository.DeleteById(id);
            _repository.Save();
        }

        public FuncionarioDTO GetFuncionario(Guid id)
        {
            var funcionario = _repository.FuncionarioRepository.FindById(id).FirstOrDefault();

            return _mapper.Map<FuncionarioDTO>(funcionario);
        }

        public List<FuncionarioDTO> GetFuncionarios()
        {
            var funcionarios = _repository.FuncionarioRepository.GetAll().ToList();

            return _mapper.Map<List<FuncionarioDTO>>(funcionarios);
        }

        public void UpdateFuncionario(FuncionarioDTO funcionario)
        {
            var funcionarioAchado = _repository.FuncionarioRepository.FindById((Guid)funcionario.Id).FirstOrDefault();

            if(funcionarioAchado is null)
            {
                throw new EntidadeNaoEncontrada("Funcionário");
            }

            funcionarioAchado.Apelido = funcionario.Apelido;
            funcionarioAchado.Senha = funcionario.Senha;
            funcionarioAchado.Imagem = funcionario.Imagem;
            funcionarioAchado.Nome = funcionario.Nome;

            _repository.FuncionarioRepository.Update(funcionarioAchado);
            _repository.Save();
        }
    }
}
