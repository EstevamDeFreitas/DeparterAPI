using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    [Serializable]
    public class FuncionarioNaoPodeSeRemover : Exception
    {
        public FuncionarioNaoPodeSeRemover() : base("Funcionário não pode se remover do departamento") { }
    }
}
