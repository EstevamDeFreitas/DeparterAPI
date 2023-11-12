using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("equipes")]
    public class Equipe : EntityBase
    {
        [Required]
        [Column("nome")]
        public string Nome { get; set; }
        [Column("descricao")]
        public string Descricao { get; set; }
        [Column("maximo_horas_diarias")]
        public int MaximoHorasDiarias { get; set; }
        [Column("maximo_horas_mensais")]
        public int MaximoHorasMensais { get; set; }
        [Column("image_url")]
        public string ImageUrl { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_onscreen")]
        public int OnScreenId { get; set; }

        public ICollection<EquipeUsuario> EquipeUsuarios { get; set; }
        public ICollection<Atividade> Atividades { get; set; }
    }
}
