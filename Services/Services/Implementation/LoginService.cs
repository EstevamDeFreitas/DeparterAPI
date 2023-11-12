using AutoMapper;
using Persistence.Repositories.Interfaces;
using Services.DTO;
using Services.Exceptions;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Implementation
{
    public class LoginService : ServiceBase, ILoginService
    {
        public LoginService(IRepositoryWrapper repository, IMapper mapper, IServiceWrapper serviceWrapper) : base(repository, mapper, serviceWrapper)
        {
        }

        public string Login(LoginDTO login)
        {
            var senhaHash = "";

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(login.Senha));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                senhaHash = builder.ToString();
            }

            var usuario = _repository.UsuarioRepository.FindByCondition(x => x.Email == login.Email && x.Senha == senhaHash).FirstOrDefault();

            if(usuario is null)
            {
                throw new EmailOuSenhaIncorretos();
            }

            return this._serviceWrapper.TokenService.GenerateToken(usuario);

        }
    }
}
