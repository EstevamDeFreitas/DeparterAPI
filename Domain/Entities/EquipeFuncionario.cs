using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("equipe_usuarios")]
    public class EquipeUsuario
    {
        [Column("id_equipe")]
        public Guid EquipeId { get; set; }
        [Column("id_usuario")]
        public Guid UsuarioId { get; set; }

        public Equipe Equipe { get; set; }
        public Usuario Usuario { get; set; }
    }
}
