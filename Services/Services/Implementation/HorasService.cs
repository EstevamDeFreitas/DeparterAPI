using AutoMapper;
using Domain.Entities;
using Persistence.Repositories.Interfaces;
using Services.DTO;
using Services.Exceptions;
using Services.Services.Interfaces;
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

        public void CreateFuncionarioHorasConfiguracao(FuncionarioHorasConfiguracaoCreateDTO funcionarioHorasConfiguracao)
        {
            var funcionarioHorasConfiguracaoCreate = _mapper.Map<FuncionarioHorasConfiguracao>(funcionarioHorasConfiguracao);

            funcionarioHorasConfiguracaoCreate.Gerar();

            _repository.FuncionarioHorasConfiguracaoRepository.Create(funcionarioHorasConfiguracaoCreate);
            _repository.Save();
        }

        public void CreateHoras(FuncionarioAtividadeHorasCreateDTO funcionarioAtividadeHoras)
        {
            var horas = _mapper.Map<FuncionarioAtividadeHoras>(funcionarioAtividadeHoras);

            horas.Gerar();

            //TODO adicionar verificação das horas atuais do funcionario (dia, mes e departamento)

            _repository.AtividadeHorasRepository.Create(horas);
            _repository.Save();
        }

        public void DeleteFuncionarioHorasConfiguracao(Guid horaConfiguracaoId)
        {
            _repository.FuncionarioHorasConfiguracaoRepository.DeleteById(horaConfiguracaoId);
            _repository.Save();
        }

        public void DeleteHoras(Guid horaId)
        {
            _repository.AtividadeHorasRepository.DeleteById(horaId);
            _repository.Save();
        }

        public List<FuncionarioAtividadeHorasDTO> GetAtividadeHoras(Guid atividadeId)
        {
            var horas = _repository.AtividadeHorasRepository.FindFullByCondition(x => x.AtividadeId == atividadeId).ToList();

            return _mapper.Map<List<FuncionarioAtividadeHorasDTO>>(horas);
        }

        public List<FuncionarioAtividadeHorasDTO> GetFuncionarioHoras(Guid funcionarioId)
        {
            var horas = _repository.AtividadeHorasRepository.FindFullByCondition(x => x.FuncionarioId == funcionarioId).ToList();

            return _mapper.Map<List<FuncionarioAtividadeHorasDTO>>(horas);
        }

        public List<FuncionarioAtividadeHorasDTO> GetFuncionarioAtividadeHoras(Guid funcionarioId, Guid atividadeId)
        {
            var horas = _repository.AtividadeHorasRepository.FindFullByCondition(x => x.FuncionarioId == funcionarioId && x.AtividadeId == atividadeId);

            return _mapper.Map<List<FuncionarioAtividadeHorasDTO>>(horas);
        }

        public List<FuncionarioHorasConfiguracaoDTO> GetFuncionarioHorasConfiguracoes(Guid funcionarioId)
        {
            var configuracoes = _repository.FuncionarioHorasConfiguracaoRepository.FindByCondition(x => x.FuncionarioId == funcionarioId).ToList();

            return _mapper.Map<List<FuncionarioHorasConfiguracaoDTO>>(configuracoes);
        }

        public List<FuncionarioAtividadeHorasDTO> GetHoras()
        {
            var horas = _repository.AtividadeHorasRepository.FindFull().ToList();

            return _mapper.Map<List<FuncionarioAtividadeHorasDTO>>(horas);
        }

        public void UpdateFuncionarioHorasConfiguracao(FuncionarioHorasConfiguracaoUpdateDTO funcionarioHorasConfiguracao)
        {
            var horasConfiguracao = _repository.FuncionarioHorasConfiguracaoRepository.FindByCondition(x => x.Id == funcionarioHorasConfiguracao.Id).FirstOrDefault();
            if (horasConfiguracao is null)
            {
                throw new EntidadeNaoEncontrada("Funcionario Horas Configuracao");
            }


            horasConfiguracao.Minutos = funcionarioHorasConfiguracao.Minutos;
            horasConfiguracao.TipoConfiguracao = funcionarioHorasConfiguracao.TipoConfiguracao;
            horasConfiguracao.FuncionarioId = funcionarioHorasConfiguracao.FuncionarioId;

            _repository.FuncionarioHorasConfiguracaoRepository.Update(horasConfiguracao);
            _repository.Save();
        }

        public void UpdateHoras(FuncionarioAtividadeHorasUpdateDTO funcionarioAtividadeHoras)
        {
            var horasFound = _repository.AtividadeHorasRepository.FindById(funcionarioAtividadeHoras.Id).FirstOrDefault();

            horasFound.AtividadeId = funcionarioAtividadeHoras.AtividadeId;
            horasFound.FuncionarioId = funcionarioAtividadeHoras.FuncionarioId;
            horasFound.Minutos = funcionarioAtividadeHoras.Minutos;

            _repository.AtividadeHorasRepository.Update(horasFound);
            _repository.Save();
        }

        public HorasResumo GetHorasResumo(Guid? funcionarioId, Guid? departamentoId)
        {
            var funcionarioHoras = _repository.AtividadeHorasRepository.FindFullByCondition(x => (funcionarioId.HasValue ? funcionarioId == x.FuncionarioId : true) && (departamentoId.HasValue ? departamentoId == x.Atividade.DepartamentoId : true)).ToList();

            var dtHoje = DateTime.Now;

            HorasResumo result = new HorasResumo();

            try
            {
                result = new HorasResumo
                {
                    MediaMensalMinutos = ((int)funcionarioHoras.GroupBy(x => new { x.DataCriacao.Month, x.DataCriacao.Year }).Select(x => x.Sum(y => y.Minutos)).Average(x => x)),
                    MinutosHoje = funcionarioHoras.Where(x => x.DataCriacao.Day == dtHoje.Day && x.DataCriacao.Month == dtHoje.Month && x.DataCriacao.Year == dtHoje.Year).Sum(x => x.Minutos),
                    MinutosMesPassado = funcionarioHoras.Where(x => x.DataCriacao.Year == dtHoje.AddMonths(-1).Year && x.DataCriacao.Month == dtHoje.AddMonths(-1).Month).Sum(x => x.Minutos),
                    MinutosMesVigente = funcionarioHoras.Where(x => x.DataCriacao.Year == dtHoje.Year && x.DataCriacao.Month == dtHoje.Month).Sum(x => x.Minutos),
                };
            }
            catch(Exception ex)
            {

            }

            

            try
            {
                //TODO corrigir para varios funcionarios diferentes
                var regrasHoras = funcionarioHoras.SelectMany(x => x.Funcionario.FuncionarioHorasConfiguracaos).ToList();

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

        public List<HorasCategoria> GetHorasCategorias(Guid? funcionarioId, Guid? departamentoId)
        {
            var funcionarioHoras = _repository.AtividadeHorasRepository.FindFullByCondition(x => (funcionarioId.HasValue ? funcionarioId == x.FuncionarioId : true) && (departamentoId.HasValue ? departamentoId == x.Atividade.DepartamentoId : true)).ToList();

            var categorias = funcionarioHoras.SelectMany(x => x.Atividade.AtividadeCategorias).Select(x => x.Categoria).Distinct().ToList();

            var horasCategorias = new List<HorasCategoria>();

            categorias.ForEach(categoria =>
            {
                var horas = funcionarioHoras.Where(x => x.Atividade.AtividadeCategorias.Any(y => y.CategoriaId == categoria.Id))
                                            .GroupBy(x => new {Mes = x.DataCriacao.Month, Ano = x.DataCriacao.Year})
                                            .Select(x => new ValorPorData
                                            {
                                                Data = new DateTime(x.Key.Ano, x.Key.Mes, 1),
                                                Valor = x.Sum(x => x.Minutos) / 60
                                            }).OrderBy(x => x.Data).ToList();

                horasCategorias.Add(new HorasCategoria
                {
                    Categoria = categoria.Nome,
                    HorasPorMes = horas
                });

                
            });



            return horasCategorias;
        }
    }
}
