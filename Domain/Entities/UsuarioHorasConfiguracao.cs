using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("usuario_horas_configuracoes")]
    public class UsuarioHorasConfiguracao : EntityBase
    {
        [Required]
        [Column("id_usuario")]
        public Guid UsuarioId { get; set; }
        [Required]
        [Column("tipo_configuracao")]
        public TipoConfigHora TipoConfiguracao { get; set; }
        [Required]
        [Column("minutos")]
        public int Minutos { get; set; }

        public Usuario Usuario { get; set; }
    }

    public enum TipoConfigHora
    {
        Diario,
        Mensal
    }
}
