﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class DepartamentoDTO
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
        public List<DepartamentoFuncionarioDTO> DepartamentoFuncionarios { get; set; }
        public List<AtividadeDTO> Atividades { get; set; }
    }

    public class DepartamentoAtividadesResumoDTO
    {
        public DateTime DataEntrega { get; set; }
        public string Descricao { get; set; }
        public string Funcionario { get; set; }
        public string Status { get; set; }
        public Guid AtividadeId { get; set; }
    }

    public class DepartamentoCreateDTO
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
        public List<DepartamentoFuncionarioCreateDTO> DepartamentoFuncionarios { get; set; }
    }

    public class DepartamentoFuncionarioCreateDTO
    {
        public Guid FuncionarioId { get; set; }
    }

    public class DepartamentoFuncionarioDTO
    {
        public Guid DepartamentoId { get; set; }
        public Guid FuncionarioId { get; set; }

        public FuncionarioDTO Funcionario { get; set; }
    }
}
