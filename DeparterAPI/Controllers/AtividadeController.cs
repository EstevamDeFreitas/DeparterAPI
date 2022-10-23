using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Notations;
using Services.Services.Interfaces;
using Services.Utilities;

namespace DeparterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtividadeController : ControllerBase
    {
        protected readonly IServiceWrapper _serviceWrapper;

        public AtividadeController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetCategorias()
        {
            try
            {
                var atividades = _serviceWrapper.AtividadeService.GetAtividades();

                return Ok(new Result<List<AtividadeDTO>>("Atividades Encontradas", atividades));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetCategorias(Guid id)
        {
            try
            {
                var atividade = _serviceWrapper.AtividadeService.GetAtividade(id);

                return Ok(new Result<AtividadeDTO>("Atividade Encontrada", atividade));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateAtividade([FromBody] AtividadeCreateDTO atividade)
        {
            try
            {
                _serviceWrapper.AtividadeService.CreateAtividade(atividade, Guid.Parse(HttpContext.Items["User"].ToString()));

                return Ok(new Result<List<AtividadeDTO>>("Atividade Criada"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }
    }
}
