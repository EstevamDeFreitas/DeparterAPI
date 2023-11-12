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

            return Ok(new Result<List<UsuarioAtividadeHorasDTO>>("Horas Encontradas", horas));
        }

        [HttpGet("resumo")]
        [Authorize]
        public IActionResult GetResumo([FromQuery] Guid? usuarioId, [FromQuery] Guid? equipeId)
        {
            var resumo = _serviceWrapper.HorasService.GetHorasResumo(usuarioId, equipeId);

            return Ok(new Result<HorasResumo>("Resumo de Horas Encontrado", resumo));
        }

        [HttpGet("agrupamento/categorias")]
        [Authorize]
        public IActionResult GetHorasPorCategoria([FromQuery] Guid? usuarioId, [FromQuery] Guid? equipeId)
        {
            var horasCategorias = _serviceWrapper.HorasService.GetHorasCategorias(usuarioId, equipeId);

            return Ok(new Result<List<HorasCategoria>>("Agrupamento de horas por categoria encontrado", horasCategorias));
        }

        [HttpGet("usuario/{usuarioId}")]
        [Authorize]
        public IActionResult GetByUsuario(Guid usuarioId)
        {
            var horas = _serviceWrapper.HorasService.GetUsuarioHoras(usuarioId);

            return Ok(new Result<List<UsuarioAtividadeHorasDTO>>("Horas Encontradas", horas));
        }

        [HttpGet("atividade/{atividadeId}")]
        [Authorize]
        public IActionResult GetByAtividade(Guid atividadeId)
        {
            var horas = _serviceWrapper.HorasService.GetAtividadeHoras(atividadeId);

            return Ok(new Result<List<UsuarioAtividadeHorasDTO>>("Horas Encontradas", horas));
        }

        [HttpGet("usuario/{usuarioId}/atividade/{atividadeId}")]
        [Authorize]
        public IActionResult GetByUsuarioAndAtividade(Guid usuarioId, Guid atividadeId)
        {
            var horas = _serviceWrapper.HorasService.GetUsuarioAtividadeHoras(usuarioId, atividadeId);

            return Ok(new Result<List<UsuarioAtividadeHorasDTO>>("Horas Encontradas", horas));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] UsuarioAtividadeHorasCreateDTO usuarioAtividadeHora)
        {
            _serviceWrapper.HorasService.CreateHoras(usuarioAtividadeHora);

            return Ok(new Result<object>("Hora Criada"));
        }

        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] UsuarioAtividadeHorasUpdateDTO usuarioAtividadeHora)
        {
            _serviceWrapper.HorasService.UpdateHoras(usuarioAtividadeHora);

            return Ok(new Result<object>("Hora Atualizada"));
        }

        [HttpDelete("{horaId}")]
        [Authorize]
        public IActionResult Delete(Guid horaId)
        {
            _serviceWrapper.HorasService.DeleteHoras(horaId);

            return Ok(new Result<object>("Hora Deletada"));
        }


        [HttpGet("configuracao/usuario/{usuarioId}")]
        [Authorize]
        public IActionResult GetUsuarioConfiguracao(Guid usuarioId)
        {
            var horas = _serviceWrapper.HorasService.GetUsuarioHorasConfiguracoes(usuarioId);

            return Ok(new Result<List<UsuarioHorasConfiguracaoDTO>>("Configuração das Horas Encontradas", horas));
        }

        [HttpPost("configuracao")]
        [Authorize]
        public IActionResult PostConfiguration([FromBody] UsuarioHorasConfiguracaoCreateDTO usuarioAtividadeHora)
        {
            _serviceWrapper.HorasService.CreateUsuarioHorasConfiguracao(usuarioAtividadeHora);

            return Ok(new Result<object>("Hora Criada"));
        }

        [HttpPut("configuracao")]
        [Authorize]
        public IActionResult PutConfiguration([FromBody] UsuarioHorasConfiguracaoUpdateDTO usuarioAtividadeHora)
        {
            _serviceWrapper.HorasService.UpdateUsuarioHorasConfiguracao(usuarioAtividadeHora);

            return Ok(new Result<object>("Hora Atualizada"));
        }

        [HttpDelete("configuracao/{horaConfiguracaoId}")]
        [Authorize]
        public IActionResult DeleteConfiguration(Guid horaConfiguracaoId)
        {
            _serviceWrapper.HorasService.DeleteUsuarioHorasConfiguracao(horaConfiguracaoId);

            return Ok(new Result<object>("Hora Deletada"));
        }
    }
}
