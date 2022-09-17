using AutoMapper;
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
        public ServiceWrapper(IRepositoryWrapper repository, IMapper mapper)
        {
            _funcionarioService = new Lazy<IFuncionarioService>(() => new FuncionarioService(repository, mapper));
        }

        public IFuncionarioService FuncionarioService => _funcionarioService.Value;
    }
}
