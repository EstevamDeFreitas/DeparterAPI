using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("funcionarios")]
    public class Funcionario : EntityBase
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
        [Required]
        [Column("apelido")]
        public string Apelido { get; set; }
        [Column("imagem")]
        public string Imagem { get; set; }

        public ICollection<AtividadeFuncionario> AtividadeFuncionarios { get; set; }
        public ICollection<DepartamentoFuncionario> DepartamentoFuncionarios { get; set; }
    }
}
