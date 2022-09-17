using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class EntityBase
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("dt_modificacao")]
        public DateTime DataModificacao { get; set; }
        [Required]
        [Column("dt_criacao")]
        public DateTime DataCriacao { get; set; }

        public void Gerar()
        {
            DataModificacao = DateTime.Now;
            DataCriacao = DateTime.Now;
            Id = Guid.NewGuid();
        }
    }
}
