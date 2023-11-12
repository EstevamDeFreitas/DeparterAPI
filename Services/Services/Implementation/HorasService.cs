using AutoMapper;
using Domain.Entities;
using Persistence.Repositories.Interfaces;
using Services.DTO;
using Services.Exceptions;
using Services.Services.Interfaces;
using Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Implementation
{
    public class HorasService : ServiceBase, IHorasService
    {
        public HorasService(IRepositoryWrapper repository, IMapper mapper, IServiceWrapper serviceWrapper) : base(repository, mapper, serviceWrapper)
        {
        }

        public void CreateUsuarioHorasConfiguracao(UsuarioHorasConfiguracaoCreateDTO usuarioHorasConfiguracao)
        {
            var usuarioHorasConfiguracaoCreate = _mapper.Map<UsuarioHorasConfiguracao>(usuarioHorasConfiguracao);

            usuarioHorasConfiguracaoCreate.Gerar();

            _repository.UsuarioHorasConfiguracaoRepository.Create(usuarioHorasConfiguracaoCreate);
            _repository.Save();
        }

        public void CreateHoras(UsuarioAtividadeHorasCreateDTO usuarioAtividadeHoras)
        {
            var horas = _mapper.Map<UsuarioAtividadeHoras>(usuarioAtividadeHoras);

            horas.Gerar();

            
            var resumo = GetHorasResumo(usuarioAtividadeHoras.UsuarioId, null);

            _repository.AtividadeHorasRepository.Create(horas);
            _repository.Save();

            //TODO adicionar verificação das horas atuais do usuario (dia, mes e equipe)
            if (resumo.MinutosHojeRestantes - usuarioAtividadeHoras.Minutos < 0 || resumo.MinutosMesRestantes - usuarioAtividadeHoras.Minutos < 0)
            {
                
            }
        }

        public void DeleteUsuarioHorasConfiguracao(Guid horaConfiguracaoId)
        {
            _repository.UsuarioHorasConfiguracaoRepository.DeleteById(horaConfiguracaoId);
            _repository.Save();
        }

        public void DeleteHoras(Guid horaId)
        {
            _repository.AtividadeHorasRepository.DeleteById(horaId);
            _repository.Save();
        }

        public List<UsuarioAtividadeHorasDTO> GetAtividadeHoras(Guid atividadeId)
        {
            var horas = _repository.AtividadeHorasRepository.FindFullByCondition(x => x.AtividadeId == atividadeId).ToList();

            return _mapper.Map<List<UsuarioAtividadeHorasDTO>>(horas);
        }

        public List<UsuarioAtividadeHorasDTO> GetUsuarioHoras(Guid usuarioId)
        {
            var horas = _repository.AtividadeHorasRepository.FindFullByCondition(x => x.UsuarioId == usuarioId).ToList();

            return _mapper.Map<List<UsuarioAtividadeHorasDTO>>(horas);
        }

        public List<UsuarioAtividadeHorasDTO> GetUsuarioAtividadeHoras(Guid usuarioId, Guid atividadeId)
        {
            var horas = _repository.AtividadeHorasRepository.FindFullByCondition(x => x.UsuarioId == usuarioId && x.AtividadeId == atividadeId);

            return _mapper.Map<List<UsuarioAtividadeHorasDTO>>(horas);
        }

        public List<UsuarioHorasConfiguracaoDTO> GetUsuarioHorasConfiguracoes(Guid usuarioId)
        {
            var configuracoes = _repository.UsuarioHorasConfiguracaoRepository.FindByCondition(x => x.UsuarioId == usuarioId).ToList();

            return _mapper.Map<List<UsuarioHorasConfiguracaoDTO>>(configuracoes);
        }

        public List<UsuarioAtividadeHorasDTO> GetHoras()
        {
            var horas = _repository.AtividadeHorasRepository.FindFull().ToList();

            return _mapper.Map<List<UsuarioAtividadeHorasDTO>>(horas);
        }

        public void UpdateUsuarioHorasConfiguracao(UsuarioHorasConfiguracaoUpdateDTO usuarioHorasConfiguracao)
        {
            var horasConfiguracao = _repository.UsuarioHorasConfiguracaoRepository.FindByCondition(x => x.Id == usuarioHorasConfiguracao.Id).FirstOrDefault();
            if (horasConfiguracao is null)
            {
                throw new EntidadeNaoEncontrada("Usuario Horas Configuracao");
            }


            horasConfiguracao.Minutos = usuarioHorasConfiguracao.Minutos;
            horasConfiguracao.TipoConfiguracao = usuarioHorasConfiguracao.TipoConfiguracao;
            horasConfiguracao.UsuarioId = usuarioHorasConfiguracao.UsuarioId;

            _repository.UsuarioHorasConfiguracaoRepository.Update(horasConfiguracao);
            _repository.Save();
        }

        public void UpdateHoras(UsuarioAtividadeHorasUpdateDTO usuarioAtividadeHoras)
        {
            var horasFound = _repository.AtividadeHorasRepository.FindById(usuarioAtividadeHoras.Id).FirstOrDefault();

            horasFound.AtividadeId = usuarioAtividadeHoras.AtividadeId;
            horasFound.UsuarioId = usuarioAtividadeHoras.UsuarioId;
            horasFound.Minutos = usuarioAtividadeHoras.Minutos;

            _repository.AtividadeHorasRepository.Update(horasFound);
            _repository.Save();
        }

        public HorasResumo GetHorasResumo(Guid? usuarioId, Guid? equipeId)
        {
            var usuarioHoras = _repository.AtividadeHorasRepository.FindFullByCondition(x => (usuarioId.HasValue ? usuarioId == x.UsuarioId : true) && (equipeId.HasValue ? equipeId == x.Atividade.EquipeId : true)).ToList();

            var dtHoje = DateTime.Now;

            HorasResumo result = new HorasResumo();

            try
            {
                result = new HorasResumo
                {
                    MediaMensalMinutos = ((int)usuarioHoras.GroupBy(x => new { x.DataCriacao.Month, x.DataCriacao.Year }).Select(x => x.Sum(y => y.Minutos)).Average(x => x)),
                    MinutosHoje = usuarioHoras.Where(x => x.DataCriacao.Day == dtHoje.Day && x.DataCriacao.Month == dtHoje.Month && x.DataCriacao.Year == dtHoje.Year).Sum(x => x.Minutos),
                    MinutosMesPassado = usuarioHoras.Where(x => x.DataCriacao.Year == dtHoje.AddMonths(-1).Year && x.DataCriacao.Month == dtHoje.AddMonths(-1).Month).Sum(x => x.Minutos),
                    MinutosMesVigente = usuarioHoras.Where(x => x.DataCriacao.Year == dtHoje.Year && x.DataCriacao.Month == dtHoje.Month).Sum(x => x.Minutos),
                };
            }
            catch(Exception ex)
            {

            }

            

            try
            {
                //TODO corrigir para varios usuarios diferentes
                var regrasHoras = usuarioHoras.SelectMany(x => x.Usuario.UsuarioHorasConfiguracaos).ToList();

                var horasDiariasRegra = regrasHoras.First(x => x.TipoConfiguracao == TipoConfigHora.Diario);
                var horasMensaisRegra = regrasHoras.First(x => x.TipoConfiguracao == TipoConfigHora.Mensal);

                if (horasDiariasRegra is not null)
                {
                    result.MinutosHojeRestantes = horasDiariasRegra.Minutos - result.MinutosHoje;
                }

                if (horasMensaisRegra is not null)
                {
                    result.MinutosMesRestantes = horasMensaisRegra.Minutos - result.MinutosMesVigente;
                }
            }
            catch(Exception ex)
            {

            }
            

            return result;
        }

        public List<HorasCategoria> GetHorasCategorias(Guid? usuarioId, Guid? equipeId)
        {
            var usuarioHoras = _repository.AtividadeHorasRepository.FindFullByCondition(x => (usuarioId.HasValue ? usuarioId == x.UsuarioId : true) && (equipeId.HasValue ? equipeId == x.Atividade.EquipeId : true)).ToList();

            var categorias = usuarioHoras.SelectMany(x => x.Atividade.AtividadeCategorias).Select(x => x.Categoria).Distinct().ToList();

            var horasCategorias = new List<HorasCategoria>();

            categorias.ForEach(categoria =>
            {
                var horas = usuarioHoras.Where(x => x.Atividade.AtividadeCategorias.Any(y => y.CategoriaId == categoria.Id))
                                            .GroupBy(x => new {Mes = x.DataCriacao.Month, Ano = x.DataCriacao.Year})
                                            .Select(x => new ValorPorData
                                            {
                                                Data = new DateTime(x.Key.Ano, x.Key.Mes, 1),
                                                Valor = x.Sum(x => x.Minutos) / 60
                                            }).ToList();

                horasCategorias.Add(new HorasCategoria
                {
                    Categoria = categoria.Nome,
                    HorasPorMes = horas
                });
            });


            if(horasCategorias.Count > 0)
            {
                var dates = DateHelper.GetDatesInPeriod(horasCategorias.SelectMany(x => x.HorasPorMes).Min(x => x.Data), horasCategorias.SelectMany(x => x.HorasPorMes).Max(x => x.Data));

                dates.ForEach(date =>
                {
                    horasCategorias.ForEach(cat =>
                    {
                        if (!cat.HorasPorMes.Any(x => x.Data == date))
                        {
                            cat.HorasPorMes.Add(new ValorPorData { Data = date, Valor = 0 });
                        }
                    });
                });

                horasCategorias.ForEach(hor =>
                {
                    hor.HorasPorMes = hor.HorasPorMes.OrderBy(x => x.Data).ToList();
                });
            }
            

            return horasCategorias;
        }
    }
}
