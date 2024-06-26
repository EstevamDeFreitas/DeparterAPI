﻿using AutoMapper;
using Domain.Entities;
using Persistence.Repositories.Interfaces;
using Services.DTO;
using Services.Exceptions;
using Services.Middleware;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

            var senhaHash = "";

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(funcionario.Senha));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); 
                }

                senhaHash =  builder.ToString();
            }

            funcionarioCreate.Senha = senhaHash;

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
            var resposta = _mapper.Map<FuncionarioDTO>(funcionario);

            resposta.Senha = "";

            return resposta;
        }

        public List<FuncionarioDTO> GetFuncionarios()
        {
            var funcionarios = _repository.FuncionarioRepository.GetAll().ToList();
            var resposta = _mapper.Map<List<FuncionarioDTO>>(funcionarios);

            resposta.ForEach(res =>
            {
                res.Senha = "";
            });

            return resposta;
        }

        public void UpdateFuncionario(FuncionarioDTO funcionario)
        {
            var funcionarioAchado = _repository.FuncionarioRepository.FindById((Guid)funcionario.Id).FirstOrDefault();

            if(funcionarioAchado is null)
            {
                throw new EntidadeNaoEncontrada("Funcionário");
            }

            funcionarioAchado.Apelido = funcionario.Apelido;

            if(funcionario.Senha != "")
            {
                var senhaHash = "";

                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(funcionario.Senha));

                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }

                    senhaHash = builder.ToString();
                }

                funcionarioAchado.Senha = senhaHash;
            }

            funcionarioAchado.Imagem = funcionario.Imagem;
            funcionarioAchado.Nome = funcionario.Nome;
            funcionarioAchado.IsAdmin = funcionario.IsAdmin;

            _repository.FuncionarioRepository.Update(funcionarioAchado);
            _repository.Save();
        }
    }
}
