using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class AtividadeCreateDTO
    {
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public DateTime DataEntrega { get; set; }
        [Required]
        public int TempoPrevisto { get; set; }
        public Guid? AtividadePaiId { get; set; }

        public List<Guid> Categorias { get; set; }
        public List<AtividadeFuncionarioCreateDTO> AtividadeFuncionarios { get; set; }
    }

    public class AtividadeFuncionarioCreateDTO
    {
        [Required]
        public string FuncionarioEmail { get; set; }
        [Required]
        public NivelAcesso NivelAcesso { get; set; }
    }

    public class AtividadeDTO
    {
        public Guid? Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public DateTime DataEntrega { get; set; }
        [Required]
        public int TempoPrevisto { get; set; }
        public Guid? AtividadePaiId { get; set; }

        public List<AtividadeCategoriaDTO> AtividadeCategorias { get; set; }
        public List<AtividadeFuncionarioDTO> AtividadeFuncionarios { get; set; }
        public List<AtividadeDTO> Atividades { get; set; }
    }

    public class AtividadeCategoriaDTO
    {
        [Required]
        public Guid AtividadeId { get; set; }
        [Required]
        public Guid CategoriaId { get; set; }
    }

    public class AtividadeFuncionarioDTO
    {
        [Required]
        public Guid AtividadeId { get; set; }
        [Required]
        public Guid FuncionarioId { get; set; }
        public string FuncionarioEmail { get; set; }
        [Required]
        public NivelAcesso NivelAcesso { get; set; }
    }
}
