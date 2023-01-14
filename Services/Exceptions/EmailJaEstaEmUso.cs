using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    [Serializable]
    public class EmailJaEstaEmUso : Exception
    {
        public EmailJaEstaEmUso(string email):base($"O email {email} já está em uso") { }
    }
}
