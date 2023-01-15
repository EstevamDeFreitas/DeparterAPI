using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("departamento_atividades")]
    public class DepartamentoAtividade
    {
        [Column("id_atividade")]
        public Guid AtividadeId { get; set; }
        [Column("id_departamento")]
        public Guid DepartamentoId { get; set; }

        public Departamento Departamento { get; set; }
        public Atividade Atividade { get; set; }
    }
}
