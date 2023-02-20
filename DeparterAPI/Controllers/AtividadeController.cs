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
        public IActionResult GetAtividades()
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
        public IActionResult GetAtividade(Guid id)
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

        [HttpPut]
        [Authorize]
        public IActionResult UpdateAtividade([FromBody] AtividadePutDTO atividade)
        {
            try
            {
                _serviceWrapper.AtividadeService.UpdateAtividade(atividade, Guid.Parse(HttpContext.Items["User"].ToString()));

                return Ok(new Result<object>("Atividade Atualizada"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteAtividade(Guid id)
        {
            try
            {
                _serviceWrapper.AtividadeService.DeleteAtividade(id, Guid.Parse(HttpContext.Items["User"].ToString()));

                return Ok(new Result<object>("Atividade Deletada"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpPost("share")]
        [Authorize]
        public IActionResult ShareAtividade([FromBody] AtividadeAcessoFuncionario atividadeAcessoFuncionario)
        {
            try
            {
                _serviceWrapper.AtividadeService.UpdateAccessAtividade(atividadeAcessoFuncionario, Guid.Parse(HttpContext.Items["User"].ToString()));

                return Ok(new Result<object>("Acesso Atualizado Para a Atividade"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpDelete("share")]
        [Authorize]
        public IActionResult DeleteShareAtividade([FromBody] AtividadeAcessoFuncionario atividadeAcessoFuncionario)
        {
            try
            {
                _serviceWrapper.AtividadeService.DeleteAccessAtividade(atividadeAcessoFuncionario, Guid.Parse(HttpContext.Items["User"].ToString()));

                return Ok(new Result<object>("Acesso Removido Para a Atividade"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpPost("check")]
        [Authorize]
        public IActionResult CreateAtividadeCheck([FromBody] AtividadeCheckCreateDTO atividadeCheck)
        {
            try
            {
                _serviceWrapper.AtividadeService.CreateAtividadeCheck(atividadeCheck, Guid.Parse(HttpContext.Items["User"].ToString()));

                return Ok(new Result<object>("Criado Check para Atividade"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpPut("check")]
        [Authorize]
        public IActionResult UpdateAtividadeCheck([FromBody] AtividadeCheckDTO atividadeCheck)
        {
            try
            {
                _serviceWrapper.AtividadeService.UpdateAtividadeCheck(atividadeCheck, Guid.Parse(HttpContext.Items["User"].ToString()));

                return Ok(new Result<object>("Alterado Check para Atividade"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpDelete("check/{atividadeCheckId}")]
        [Authorize]
        public IActionResult DeleteAtividadeCheck(Guid atividadeCheckId)
        {
            try
            {
                _serviceWrapper.AtividadeService.DeleteAtividadeCheck(atividadeCheckId, Guid.Parse(HttpContext.Items["User"].ToString()));

                return Ok(new Result<object>("Alterado Check para Atividade"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }
    }
}
