using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("atividade_categorias")]
    public class AtividadeCategoria
    {
        [Required]
        [Column("id_atividade")]
        public Guid AtividadeId { get; set; }
        [Required]
        [Column("id_category")]
        public Guid CategoriaId { get; set; }

        public Atividade Atividade { get; set; }
        public Categoria Categoria { get; set; }
    }
}
