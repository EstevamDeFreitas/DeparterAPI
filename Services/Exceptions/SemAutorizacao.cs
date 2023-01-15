using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    [Serializable]
    public class SemAutorizacao : Exception
    {
        public SemAutorizacao() : base("Ação Negada ao Recurso")
        {

        }
    }
}
