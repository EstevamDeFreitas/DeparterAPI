using Microsoft.AspNetCore.Mvc;
using Services.DTO;
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
        public IActionResult Get()
        {
            var horas = _serviceWrapper.HorasService.GetHoras();

            return Ok(new Result<List<FuncionarioAtividadeHorasDTO>>("Horas Encontradas", horas));
        }

        [HttpGet("funcionario/{funcionarioId}")]
        public IActionResult GetByFuncionario(Guid funcionarioId)
        {
            var horas = _serviceWrapper.HorasService.GetFuncionarioHoras(funcionarioId);

            return Ok(new Result<List<FuncionarioAtividadeHorasDTO>>("Horas Encontradas", horas));
        }

        [HttpGet("atividade/{atividadeId}")]
        public IActionResult GetByAtividade(Guid atividadeId)
        {
            var horas = _serviceWrapper.HorasService.GetAtividadeHoras(atividadeId);

            return Ok(new Result<List<FuncionarioAtividadeHorasDTO>>("Horas Encontradas", horas));
        }

        [HttpPost]
        public IActionResult Post([FromBody] FuncionarioAtividadeHorasDTO funcionarioAtividadeHora)
        {
            _serviceWrapper.HorasService.CreateHoras(funcionarioAtividadeHora);

            return Ok(new Result<object>("Hora Criada"));
        }

        [HttpPut]
        public IActionResult Put([FromBody] FuncionarioAtividadeHorasDTO funcionarioAtividadeHora)
        {
            _serviceWrapper.HorasService.UpdateHoras(funcionarioAtividadeHora);

            return Ok(new Result<object>("Hora Atualizada"));
        }

        [HttpDelete("{horaId}")]
        public IActionResult Delete(Guid horaId)
        {
            _serviceWrapper.HorasService.DeleteHoras(horaId);

            return Ok(new Result<object>("Hora Deletada"));
        }


        [HttpGet("configuracao/funcionario/{funcionarioId}")]
        public IActionResult GetFuncionarioConfiguracao(Guid funcionarioId)
        {
            var horas = _serviceWrapper.HorasService.GetFuncionarioHorasConfiguracoes(funcionarioId);

            return Ok(new Result<List<FuncionarioHorasConfiguracaoDTO>>("Horas Encontradas", horas));
        }
    }
}
