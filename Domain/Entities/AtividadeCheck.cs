using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("atividade_check")]
    public class AtividadeCheck : EntityBase
    {
        [Required]
        [Column("descricao")]
        public string Descricao { get; set; }
        [Required]
        [Column("checked")]
        public bool Checked { get; set; }
        [Required]
        [Column("id_atividade")]
        public Guid AtividadeId { get; set; }

        public Atividade Atividade { get; set; }
    }
}
