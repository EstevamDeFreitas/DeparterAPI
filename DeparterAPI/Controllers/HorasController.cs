using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Notations;
using Services.Services.Interfaces;
using Services.Utilities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeparterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorasController : ControllerBase
    {
        protected readonly IServiceWrapper _serviceWrapper;

        public HorasController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var horas = _serviceWrapper.HorasService.GetHoras();

            return Ok(new Result<List<FuncionarioAtividadeHorasDTO>>("Horas Encontradas", horas));
        }

        [HttpGet("funcionario/{funcionarioId}")]
        [Authorize]
        public IActionResult GetByFuncionario(Guid funcionarioId)
        {
            var horas = _serviceWrapper.HorasService.GetFuncionarioHoras(funcionarioId);

            return Ok(new Result<List<FuncionarioAtividadeHorasDTO>>("Horas Encontradas", horas));
        }

        [HttpGet("atividade/{atividadeId}")]
        [Authorize]
        public IActionResult GetByAtividade(Guid atividadeId)
        {
            var horas = _serviceWrapper.HorasService.GetAtividadeHoras(atividadeId);

            return Ok(new Result<List<FuncionarioAtividadeHorasDTO>>("Horas Encontradas", horas));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] FuncionarioAtividadeHorasCreateDTO funcionarioAtividadeHora)
        {
            _serviceWrapper.HorasService.CreateHoras(funcionarioAtividadeHora);

            return Ok(new Result<object>("Hora Criada"));
        }

        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] FuncionarioAtividadeHorasUpdateDTO funcionarioAtividadeHora)
        {
            _serviceWrapper.HorasService.UpdateHoras(funcionarioAtividadeHora);

            return Ok(new Result<object>("Hora Atualizada"));
        }

        [HttpDelete("{horaId}")]
        [Authorize]
        public IActionResult Delete(Guid horaId)
        {
            _serviceWrapper.HorasService.DeleteHoras(horaId);

            return Ok(new Result<object>("Hora Deletada"));
        }


        [HttpGet("configuracao/funcionario/{funcionarioId}")]
        [Authorize]
        public IActionResult GetFuncionarioConfiguracao(Guid funcionarioId)
        {
            var horas = _serviceWrapper.HorasService.GetFuncionarioHorasConfiguracoes(funcionarioId);

            return Ok(new Result<List<FuncionarioHorasConfiguracaoDTO>>("Configuração das Horas Encontradas", horas));
        }

        [HttpPost("configuracao")]
        [Authorize]
        public IActionResult PostConfiguration([FromBody] FuncionarioHorasConfiguracaoCreateDTO funcionarioAtividadeHora)
        {
            _serviceWrapper.HorasService.CreateFuncionarioHorasConfiguracao(funcionarioAtividadeHora);

            return Ok(new Result<object>("Hora Criada"));
        }

        [HttpPut("configuracao")]
        [Authorize]
        public IActionResult PutConfiguration([FromBody] FuncionarioHorasConfiguracaoUpdateDTO funcionarioAtividadeHora)
        {
            _serviceWrapper.HorasService.UpdateFuncionarioHorasConfiguracao(funcionarioAtividadeHora);

            return Ok(new Result<object>("Hora Atualizada"));
        }

        [HttpDelete("configuracao/{horaConfiguracaoId}")]
        [Authorize]
        public IActionResult DeleteConfiguration(Guid horaConfiguracaoId)
        {
            _serviceWrapper.HorasService.DeleteFuncionarioHorasConfiguracao(horaConfiguracaoId);

            return Ok(new Result<object>("Hora Deletada"));
        }
    }
}
