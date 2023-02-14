using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("departamentos")]
    public class Departamento : EntityBase
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

        public ICollection<DepartamentoFuncionario> DepartamentoFuncionarios { get; set; }
        public ICollection<Atividade> Atividades { get; set; }
    }
}
