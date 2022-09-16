using Persistence.Repositories.Interfaces;
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

        public ServiceBase(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
    }
}
