using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("atividade_funcionarios")]
    public class AtividadeFuncionario
    {
        [Required]
        [Column("id_atividade")]
        public Guid AtividadeId { get; set; }
        [Required]
        [Column("id_funcionario")]
        public Guid FuncionarioId { get; set; }
        [Required]
        [Column("nivel_acesso")]
        public NivelAcesso NivelAcesso { get; set; }

        public Atividade Atividade { get; set; }
        public Funcionario Funcionario { get; set; }
    }

    public enum NivelAcesso
    {
        Ler,
        Criar,
        Alterar,
        Deletar,
        Todos
    }
}
