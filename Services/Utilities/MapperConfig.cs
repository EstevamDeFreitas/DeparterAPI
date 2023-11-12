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
            CreateMap<UsuarioDTO, Usuario>().ReverseMap();
            CreateMap<CategoriaDTO, Categoria>().ReverseMap();
            CreateMap<AtividadeDTO, Atividade>().ReverseMap();
            CreateMap<AtividadeCheckDTO, AtividadeCheck>().ReverseMap();
            CreateMap<AtividadeCheckCreateDTO, AtividadeCheck>().ReverseMap();
            CreateMap<AtividadeCategoriaDTO, AtividadeCategoria>().ReverseMap();
            CreateMap<AtividadeUsuarioDTO, AtividadeUsuario>().ReverseMap();
            CreateMap<EquipeDTO, Equipe>().ReverseMap();
            CreateMap<EquipeCreateDTO, Equipe>().ReverseMap();
            CreateMap<EquipeUsuarioDTO, EquipeUsuario>().ReverseMap();
            CreateMap<EquipeUsuarioCreateDTO, EquipeUsuario>().ReverseMap();
            CreateMap<UsuarioAtividadeHorasDTO, UsuarioAtividadeHoras>().ReverseMap();
            CreateMap<UsuarioAtividadeHorasCreateDTO, UsuarioAtividadeHoras>().ReverseMap();
            CreateMap<UsuarioAtividadeHorasUpdateDTO, UsuarioAtividadeHoras>().ReverseMap();
            CreateMap<UsuarioHorasConfiguracaoDTO, UsuarioHorasConfiguracao>().ReverseMap();
            CreateMap<UsuarioHorasConfiguracaoCreateDTO, UsuarioHorasConfiguracao>().ReverseMap();
            CreateMap<UsuarioHorasConfiguracaoUpdateDTO, UsuarioHorasConfiguracao>().ReverseMap();
        }
    }
}
