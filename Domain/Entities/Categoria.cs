using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("categorias")]
    public class Categoria : EntityBase
    {
        [Required]
        [Column("nome")]
        public string Nome { get; set; }
        [Column("cor")]
        public string Cor { get; set; }

        public ICollection<AtividadeCategoria> AtividadeCategorias { get; set; }                                                                                               
    }
}
