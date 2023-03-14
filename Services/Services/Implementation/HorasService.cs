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
            var horas = _repository.AtividadeHorasRepository.GetAll().ToList();

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
    }
}
