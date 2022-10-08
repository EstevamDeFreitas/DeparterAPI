using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    [Serializable]
    public class EmailOuSenhaIncorretos : Exception
    {
        public EmailOuSenhaIncorretos() : base("Usuário e/ou senha estão incorretos")
        {

        }
    }
}
