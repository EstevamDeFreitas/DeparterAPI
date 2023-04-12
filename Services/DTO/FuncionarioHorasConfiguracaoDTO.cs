using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class FuncionarioHorasConfiguracaoDTO
    {
        public Guid? Id { get; set; }
        [Required]
        public Guid FuncionarioId { get; set; }
        [Required]
        public TipoConfigHora TipoConfiguracao { get; set; }
        [Required]
        public int Minutos { get; set; }

        public FuncionarioDTO? Funcionario { get; set; }
    }

    public class FuncionarioAtividadeHorasDTO
    {
        [Required]
        public Guid FuncionarioId { get; set; }
        [Required]
        public Guid AtividadeId { get; set; }
        public Guid? Id { get; set; }
        [Required]
        public int Minutos { get; set; }

        public DateTime? DataModificacao { get; set; }
        public DateTime? DataCriacao { get; set; }

        public FuncionarioDTO? Funcionario { get; set; }
        public AtividadeDTO? Atividade { get; set; }
    }

    public class FuncionarioAtividadeHorasCreateDTO
    {
        [Required]
        public Guid FuncionarioId { get; set; }
        [Required]
        public Guid AtividadeId { get; set; }
        [Required]
        public int Minutos { get; set; }
    }

    public class FuncionarioAtividadeHorasUpdateDTO
    {
        [Required]
        public Guid FuncionarioId { get; set; }
        [Required]
        public Guid AtividadeId { get; set; }
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int Minutos { get; set; }
    }

    public class FuncionarioHorasConfiguracaoCreateDTO
    {
        [Required]
        public Guid FuncionarioId { get; set; }
        [Required]
        public TipoConfigHora TipoConfiguracao { get; set; }
        [Required]
        public int Minutos { get; set; }
    }

    public class FuncionarioHorasConfiguracaoUpdateDTO
    {
        public Guid? Id { get; set; }
        [Required]
        public Guid FuncionarioId { get; set; }
        [Required]
        public TipoConfigHora TipoConfiguracao { get; set; }
        [Required]
        public int Minutos { get; set; }
    }

    public class HorasResumo
    {
        public int MinutosMesPassado { get; set; }
        public int MediaMensalMinutos { get; set; }
        public int MinutosMesVigente { get; set; }
        public int MinutosMesRestantes { get; set; }
        public int MinutosHoje { get; set; }
        public int MinutosHojeRestantes { get; set; }
    }

    public class HorasCategoria
    {
        public string Categoria { get; set; }
        public List<ValorPorData> HorasPorMes { get; set; }
    }

    public class ValorPorData
    {
        public DateTime Data { get; set; }
        public int Valor { get; set; }
    }
}
