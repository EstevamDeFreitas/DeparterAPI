using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FuncionarioHorasConfiguracao : EntityBase
    {
        public Guid FuncionarioId { get; set; }
        public TipoConfigHora TipoConfiguracao { get; set; }
        public int Minutos { get; set; }

        public Funcionario Funcionario { get; set; }
    }

    public enum TipoConfigHora
    {
        Diario,
        Mensal
    }
}
