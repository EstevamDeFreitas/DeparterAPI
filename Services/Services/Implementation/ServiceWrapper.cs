using AutoMapper;
using Microsoft.Extensions.Configuration;
using Persistence.Repositories.Interfaces;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Implementation
{
    public class ServiceWrapper : IServiceWrapper
    {
        private readonly Lazy<IFuncionarioService> _funcionarioService;
        private readonly Lazy<ITokenService> _tokenService;
        private readonly Lazy<ILoginService> _loginService;
        public ServiceWrapper(IRepositoryWrapper repository, IMapper mapper, IConfiguration configuration)
        {
            _funcionarioService = new Lazy<IFuncionarioService>(() => new FuncionarioService(repository, mapper, this));
            _tokenService = new Lazy<ITokenService>(() => new TokenService(configuration));
            _loginService = new Lazy<ILoginService>(() => new LoginService(repository, mapper, this));
        }

        public IFuncionarioService FuncionarioService => _funcionarioService.Value;
        public ITokenService TokenService => _tokenService.Value;
        public ILoginService LoginService => _loginService.Value;
    }
}
