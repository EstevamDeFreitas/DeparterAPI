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
        [Required]
        public Guid EquipeId { get; set; }

        public List<Guid> Categorias { get; set; }

        public List<AtividadeUsuarioCreateDTO> AtividadeUsuarios { get; set; }
    }

    public class AtividadeUsuarioCreateDTO
    {
        [Required]
        public string UsuarioEmail { get; set; }
        [Required]
        public NivelAcesso NivelAcesso { get; set; }
    }

    public class AtividadeAcessoUsuario
    {
        [Required]
        public Guid UsuarioId { get; set; }
        [Required]
        public Guid AtividadeId { get; set; }
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
        [Required]
        public Guid EquipeId { get; set; }
        [Required]
        public StatusAtividade StatusAtividade { get; set; }
        public DateTime DataCriacao { get; set; }
        public int OnScreenId { get; set; }


        public List<AtividadeCheckDTO> AtividadeChecks { get; set; }
        public List<AtividadeCategoriaDTO> AtividadeCategorias { get; set; }
        public List<AtividadeUsuarioDTO> AtividadeUsuarios { get; set; }
        public List<AtividadeDTO> Atividades { get; set; }
        public EquipeDTO Equipe { get; set; }
    }

    public class AtividadePutDTO
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
        [Required]
        public Guid EquipeId { get; set; }
        [Required]
        public StatusAtividade StatusAtividade { get; set; }


        public List<AtividadeCategoriaDTO> AtividadeCategorias { get; set; }
        public List<AtividadeUsuarioDTO> AtividadeUsuarios { get; set; }
    }

    public class AtividadeCategoriaDTO
    {
        [Required]
        public Guid AtividadeId { get; set; }
        [Required]
        public Guid CategoriaId { get; set; }
    }

    public class AtividadeUsuarioDTO
    {
        [Required]
        public Guid AtividadeId { get; set; }
        [Required]
        public Guid UsuarioId { get; set; }
        public string UsuarioEmail { get; set; }
        [Required]
        public NivelAcesso NivelAcesso { get; set; }
    }

    public class AtividadeCheckDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public bool Checked { get; set; }
        [Required]
        public Guid AtividadeId { get; set; }
    }

    public class AtividadeCheckCreateDTO
    {
        [Required]
        public string Descricao { get; set; }
        [Required]
        public bool Checked { get; set; }
        [Required]
        public Guid AtividadeId { get; set; }
    }

    public enum TempoBusca
    {
        Dia,
        Semana,
        Mes,
        SeisMeses,
        Ano,
        Tudo
    }

    public static class Tempo
    {
        public static DateTime? GetMaxTempo(TempoBusca tipo)
        {
            var currentDate = DateTime.Now;

            switch (tipo)
            {
                case TempoBusca.Dia: return DateTime.Now;
                case TempoBusca.Semana: return currentDate.AddDays((int)currentDate.DayOfWeek * -1);
                case TempoBusca.Mes: return new DateTime(currentDate.Year, currentDate.Month, 1);
                case TempoBusca.SeisMeses: return currentDate.AddMonths(-6);
                case TempoBusca.Ano: return currentDate.AddYears(-1);

                default: return null;
            }
        }
    }

    public class ResumoAtividades
    {
        public int Finalizadas { get; set; }
        public int Atrasadas { get; set; }
        public int Pendente { get; set; }
        public int EmDesenvolvimento { get; set; }
    }
}
