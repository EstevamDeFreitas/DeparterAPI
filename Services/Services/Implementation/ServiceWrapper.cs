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
        private readonly Lazy<IUsuarioService> _usuarioService;
        private readonly Lazy<ITokenService> _tokenService;
        private readonly Lazy<ILoginService> _loginService;
        private readonly Lazy<ICategoriaService> _categoriaService;
        private readonly Lazy<IAtividadeService> _atividadeService;
        private readonly Lazy<IEquipeService> _equipeService;
        private readonly Lazy<IHorasService> _horasService;
        public ServiceWrapper(IRepositoryWrapper repository, IMapper mapper, IConfiguration configuration)
        {
            _usuarioService = new Lazy<IUsuarioService>(() => new UsuarioService(repository, mapper, this));
            _tokenService = new Lazy<ITokenService>(() => new TokenService(configuration));
            _loginService = new Lazy<ILoginService>(() => new LoginService(repository, mapper, this));
            _categoriaService = new Lazy<ICategoriaService>(() => new CategoriaService(repository, mapper, this));
            _atividadeService = new Lazy<IAtividadeService>(() => new AtividadeService(repository, mapper, this));

            _equipeService = new Lazy<IEquipeService>(() => new EquipeService(repository, mapper, this));

            _horasService = new Lazy<IHorasService>(() => new HorasService(repository, mapper, this));
        }

        public IUsuarioService UsuarioService => _usuarioService.Value;
        public ITokenService TokenService => _tokenService.Value;
        public ILoginService LoginService => _loginService.Value;
        public ICategoriaService CategoriaService => _categoriaService.Value;
        public IAtividadeService AtividadeService => _atividadeService.Value;
        public IEquipeService EquipeService =>  _equipeService.Value;
        public IHorasService HorasService => _horasService.Value;
    }
}
