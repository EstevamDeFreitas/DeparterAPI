using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    [Serializable]
    public class EntidadeNaoEncontrada : Exception
    {
        public EntidadeNaoEncontrada(string entityName) : base($"A entidade {entityName}, não foi econtrada") { }
    }
}
