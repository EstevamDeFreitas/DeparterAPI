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
    public class UsuarioHorasConfiguracaoDTO
    {
        public Guid? Id { get; set; }
        [Required]
        public Guid UsuarioId { get; set; }
        [Required]
        public TipoConfigHora TipoConfiguracao { get; set; }
        [Required]
        public int Minutos { get; set; }

        public UsuarioDTO? Usuario { get; set; }
    }

    public class UsuarioAtividadeHorasDTO
    {
        [Required]
        public Guid UsuarioId { get; set; }
        [Required]
        public Guid AtividadeId { get; set; }
        public Guid? Id { get; set; }
        [Required]
        public int Minutos { get; set; }

        public DateTime? DataModificacao { get; set; }
        public DateTime? DataCriacao { get; set; }

        public UsuarioDTO? Usuario { get; set; }
        public AtividadeDTO? Atividade { get; set; }
    }

    public class UsuarioAtividadeHorasCreateDTO
    {
        [Required]
        public Guid UsuarioId { get; set; }
        [Required]
        public Guid AtividadeId { get; set; }
        [Required]
        public int Minutos { get; set; }
    }

    public class UsuarioAtividadeHorasUpdateDTO
    {
        [Required]
        public Guid UsuarioId { get; set; }
        [Required]
        public Guid AtividadeId { get; set; }
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int Minutos { get; set; }
    }

    public class UsuarioHorasConfiguracaoCreateDTO
    {
        [Required]
        public Guid UsuarioId { get; set; }
        [Required]
        public TipoConfigHora TipoConfiguracao { get; set; }
        [Required]
        public int Minutos { get; set; }
    }

    public class UsuarioHorasConfiguracaoUpdateDTO
    {
        public Guid? Id { get; set; }
        [Required]
        public Guid UsuarioId { get; set; }
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
