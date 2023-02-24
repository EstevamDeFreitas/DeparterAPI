using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("funcionario_atividade_horas")]
    public class FuncionarioAtividadeHoras
    {
        [Required]
        [Column("id_funcionario")]
        public Guid FuncionarioId { get; set; }
        [Required]
        [Column("id_atividade")]
        public Guid AtividadeId { get; set; }
        [Required]
        [Column("minutos")]
        public int Minutos { get; set; }

        public Funcionario Funcionario { get; set; }
        public Atividade Atividade { get; set; }
    }
}
