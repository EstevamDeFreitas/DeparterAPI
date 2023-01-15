using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("departamento_funcionarios")]
    public class DepartamentoFuncionario
    {
        [Column("id_departamento")]
        public Guid DepartamentoId { get; set; }
        [Column("id_funcionario")]
        public Guid FuncionarioId { get; set; }

        public Departamento Departamento { get; set; }
        public Funcionario Funcionario { get; set; }
    }
}
