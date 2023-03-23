using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    [Serializable]
    public class CriadorNaoPodeSerRemovido : Exception
    {
        public CriadorNaoPodeSerRemovido() : base("O criador da atividade não pode ser removido da mesma")
        {

        }
    }
}
