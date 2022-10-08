using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("atividades")]
    public class Atividade : EntityBase
    {
        [Required]
        [Column("titulo")]
        public string Titulo { get; set; }
        [Required]
        [Column("descricao")]
        public string Descricao { get; set; }
        [Required]
        [Column("dt_entrega")]
        public DateTime DataEntrega { get; set; }
        //Tempo considerado em minutos
        [Required]
        [Column("tempo_previsto")]
        public int TempoPrevisto { get; set; }

        public ICollection<AtividadeCategoria> AtividadeCategorias { get; set; }
        public ICollection<AtividadeFuncionario> AtividadeFuncionarios { get; set; }
    }
}
