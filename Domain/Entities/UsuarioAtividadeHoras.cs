using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("usuario_atividade_horas")]
    public class UsuarioAtividadeHoras : EntityBase
    {
        [Required]
        [Column("id_usuario")]
        public Guid UsuarioId { get; set; }
        [Required]
        [Column("id_atividade")]
        public Guid AtividadeId { get; set; }
        [Required]
        [Column("minutos")]
        public int Minutos { get; set; }

        public Usuario Usuario { get; set; }
        public Atividade Atividade { get; set; }
    }
}
