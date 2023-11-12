using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("usuarios")]
    public class Usuario : EntityBase
    {
        [Required]
        [Column("nome")]
        public string Nome { get; set; }
        [Required]
        [Column("email")]
        public string Email { get; set; }
        [Required]
        [Column("senha")]
        public string Senha { get; set; }
        [Column("imagem")]
        public string Imagem { get; set; }
        [Column("is_admin")]
        public bool IsAdmin { get; set; }

        public ICollection<AtividadeUsuario> AtividadeUsuarios { get; set; }
        public ICollection<EquipeUsuario> EquipeUsuarios { get; set; }
        public ICollection<UsuarioAtividadeHoras> UsuarioAtividadeHoras { get; set; }
        public ICollection<UsuarioHorasConfiguracao> UsuarioHorasConfiguracaos { get; set; }
    }
}
