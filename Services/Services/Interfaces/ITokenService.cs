using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(Funcionario user);
        public Guid? ValidateToken(string token);
    }
}
