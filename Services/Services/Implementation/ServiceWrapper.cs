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
        private readonly Lazy<ICategoriaService> _categoriaService;
        private readonly Lazy<IAtividadeService> _atividadeService;
        private readonly Lazy<IDepartamentoService> _departamentoService;
        private readonly Lazy<IHorasService> _horasService;
        public ServiceWrapper(IRepositoryWrapper repository, IMapper mapper, IConfiguration configuration)
        {
            _funcionarioService = new Lazy<IFuncionarioService>(() => new FuncionarioService(repository, mapper, this));
            _tokenService = new Lazy<ITokenService>(() => new TokenService(configuration));
            _loginService = new Lazy<ILoginService>(() => new LoginService(repository, mapper, this));
            _categoriaService = new Lazy<ICategoriaService>(() => new CategoriaService(repository, mapper, this));
            _atividadeService = new Lazy<IAtividadeService>(() => new AtividadeService(repository, mapper, this));

            _departamentoService = new Lazy<IDepartamentoService>(() => new DepartamentoService(repository, mapper, this));

            _horasService = new Lazy<IHorasService>(() => new HorasService(repository, mapper, this));
        }

        public IFuncionarioService FuncionarioService => _funcionarioService.Value;
        public ITokenService TokenService => _tokenService.Value;
        public ILoginService LoginService => _loginService.Value;
        public ICategoriaService CategoriaService => _categoriaService.Value;
        public IAtividadeService AtividadeService => _atividadeService.Value;
        public IDepartamentoService DepartamentoService =>  _departamentoService.Value;
        public IHorasService HorasService => _horasService.Value;
    }
}
