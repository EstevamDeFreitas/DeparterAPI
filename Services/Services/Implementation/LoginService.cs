using AutoMapper;
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
    public class LoginService : ServiceBase, ILoginService
    {
        public LoginService(IRepositoryWrapper repository, IMapper mapper, IServiceWrapper serviceWrapper) : base(repository, mapper, serviceWrapper)
        {
        }

        public string Login(LoginDTO login)
        {
            var funcionario = _repository.FuncionarioRepository.FindByCondition(x => x.Email == login.Email && x.Senha == login.Senha).FirstOrDefault();

            if(funcionario is null)
            {
                throw new EmailOuSenhaIncorretos();
            }

            return this._serviceWrapper.TokenService.GenerateToken(funcionario);

        }
    }
}
