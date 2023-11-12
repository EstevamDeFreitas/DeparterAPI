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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Implementation
{
    public class UsuarioService : ServiceBase, IUsuarioService
    {
        public UsuarioService(IRepositoryWrapper repository, IMapper mapper, IServiceWrapper serviceWrapper) : base(repository, mapper, serviceWrapper)
        {
        }

        public void CreateUsuario(UsuarioDTO usuario)
        {
            var usuarioFound = _repository.UsuarioRepository.GetUsuariosFromEmails(new List<string> { usuario.Email }).FirstOrDefault();

            if(usuarioFound is not null)
            {
                throw new EmailJaEstaEmUso(usuario.Email);
            }

            var usuarioCreate = _mapper.Map<Usuario>(usuario);

            usuarioCreate.Gerar();

            var senhaHash = "";

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(usuario.Senha));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); 
                }

                senhaHash =  builder.ToString();
            }

            usuarioCreate.Senha = senhaHash;

            _repository.UsuarioRepository.Create(usuarioCreate);
            _repository.Save();
        }

        public void DeleteUsuario(Guid id)
        {
            _repository.UsuarioRepository.DeleteById(id);
            _repository.Save();
        }

        public UsuarioDTO GetUsuario(Guid id)
        {
            var usuario = _repository.UsuarioRepository.FindById(id).FirstOrDefault();
            var resposta = _mapper.Map<UsuarioDTO>(usuario);

            resposta.Senha = "";

            return resposta;
        }

        public List<UsuarioDTO> GetUsuarios()
        {
            var usuarios = _repository.UsuarioRepository.GetAll().ToList();
            var resposta = _mapper.Map<List<UsuarioDTO>>(usuarios);

            resposta.ForEach(res =>
            {
                res.Senha = "";
            });

            return resposta;
        }

        public void UpdateUsuario(UsuarioDTO usuario)
        {
            var usuarioAchado = _repository.UsuarioRepository.FindById((Guid)usuario.Id).FirstOrDefault();

            if(usuarioAchado is null)
            {
                throw new EntidadeNaoEncontrada("Usuário");
            }

            if(usuario.Senha != "")
            {
                var senhaHash = "";

                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(usuario.Senha));

                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }

                    senhaHash = builder.ToString();
                }

                usuarioAchado.Senha = senhaHash;
            }

            usuarioAchado.Imagem = usuario.Imagem;
            usuarioAchado.Nome = usuario.Nome;
            usuarioAchado.IsAdmin = usuario.IsAdmin;

            _repository.UsuarioRepository.Update(usuarioAchado);
            _repository.Save();
        }
    }
}
