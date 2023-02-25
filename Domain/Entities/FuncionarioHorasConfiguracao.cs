using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("funcionario_horas_configuracoes")]
    public class FuncionarioHorasConfiguracao : EntityBase
    {
        [Required]
        [Column("id_funcionario")]
        public Guid FuncionarioId { get; set; }
        [Required]
        [Column("tipo_configuracao")]
        public TipoConfigHora TipoConfiguracao { get; set; }
        [Required]
        [Column("minutos")]
        public int Minutos { get; set; }

        public Funcionario Funcionario { get; set; }
    }

    public enum TipoConfigHora
    {
        Diario,
        Mensal
    }
}
