﻿using Domain.Entities;
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

        public FuncionarioDTO? Funcionario { get; set; }
        public AtividadeDTO? Atividade { get; set; }
    }
}
