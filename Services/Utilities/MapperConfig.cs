using AutoMapper;
using Domain.Entities;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utilities
{
    public class MapperConfig : MapperConfigurationExpression
    {
        public MapperConfig()
        {
            CreateMap<FuncionarioDTO, Funcionario>().ReverseMap();
            CreateMap<CategoriaDTO, Categoria>().ReverseMap();
            CreateMap<AtividadeDTO, Atividade>().ReverseMap();
            CreateMap<AtividadeCategoriaDTO, AtividadeCategoria>().ReverseMap();
            CreateMap<AtividadeFuncionarioDTO, AtividadeFuncionario>().ReverseMap();
        }
    }
}
