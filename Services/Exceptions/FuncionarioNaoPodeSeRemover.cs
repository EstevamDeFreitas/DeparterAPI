using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    [Serializable]
    public class UsuarioNaoPodeSeRemover : Exception
    {
        public UsuarioNaoPodeSeRemover() : base("Usuário não pode se remover do equipe") { }
    }
}
