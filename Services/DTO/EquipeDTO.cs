using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class EquipeDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public int MaximoHorasDiarias { get; set; }
        [Required]
        public int MaximoHorasMensais { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        public int OnScreenId { get; set; }
        public List<EquipeUsuarioDTO> EquipeUsuarios { get; set; }
        public List<AtividadeDTO> Atividades { get; set; }
    }

    public class EquipeAtividadesResumoDTO
    {
        public DateTime DataEntrega { get; set; }
        public string Descricao { get; set; }
        public string Usuario { get; set; }
        public string Status { get; set; }
        public Guid AtividadeId { get; set; }
    }

    public class EquipeCreateDTO
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public int MaximoHorasDiarias { get; set; }
        [Required]
        public int MaximoHorasMensais { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public List<EquipeUsuarioCreateDTO> EquipeUsuarios { get; set; }
    }

    public class EquipeUsuarioCreateDTO
    {
        public Guid UsuarioId { get; set; }
    }

    public class EquipeUsuarioDTO
    {
        public Guid EquipeId { get; set; }
        public Guid UsuarioId { get; set; }

        public UsuarioDTO Usuario { get; set; }
    }
}
