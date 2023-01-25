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
            CreateMap<DepartamentoDTO, Departamento>().ReverseMap();
            CreateMap<DepartamentoCreateDTO, Departamento>().ReverseMap();
            CreateMap<DepartamentoFuncionarioDTO, DepartamentoFuncionario>().ReverseMap();
            CreateMap<DepartamentoFuncionarioCreateDTO, DepartamentoFuncionario>().ReverseMap();
            CreateMap<DepartamentoAtividadeDTO, DepartamentoAtividade>().ReverseMap();
        }
    }
}
