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
    public class ServiceBase
    {
        private protected IRepositoryWrapper _repository;
        private protected IMapper _mapper;
        private protected IServiceWrapper _serviceWrapper;
        public ServiceBase(IRepositoryWrapper repository, IMapper mapper, IServiceWrapper serviceWrapper)
        {
            _repository = repository;
            _mapper = mapper;
            _serviceWrapper = serviceWrapper;
        }
    }
}
