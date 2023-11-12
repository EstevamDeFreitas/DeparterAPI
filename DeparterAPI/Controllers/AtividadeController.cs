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
        public IActionResult GetAtividades([FromQuery]bool? isAdminSearch)
        {
            try
            {
                var atividades = _serviceWrapper.AtividadeService.GetAtividades(isAdminSearch, Guid.Parse(HttpContext.Items["User"].ToString()));

                return Ok(new Result<List<AtividadeDTO>>("Atividades Encontradas", atividades));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpGet("resumo")]
        [Authorize]
        public IActionResult GetAtividadeResumo([FromQuery]TempoBusca tempo, [FromQuery] Guid? usuarioId, [FromQuery] Guid? equipeId)
        {
            try
            {
                var resumo = _serviceWrapper.AtividadeService.GetResumoAtividades(tempo, usuarioId, equipeId);

                return Ok(new Result<ResumoAtividades>("Resumo das Atividades Encontrado", resumo));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetAtividade(Guid id, [FromQuery] bool? isAdminSearch)
        {
            try
            {
                AtividadeDTO atividade = new AtividadeDTO();

                if (isAdminSearch.HasValue && isAdminSearch == true)
                {
                    atividade = _serviceWrapper.AtividadeService.GetAtividade(id);
                }
                else
                {
                    atividade = _serviceWrapper.AtividadeService.GetAtividade(id, Guid.Parse(HttpContext.Items["User"].ToString()));
                }


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
        public IActionResult ShareAtividade([FromBody] AtividadeAcessoUsuario atividadeAcessoUsuario)
        {
            try
            {
                _serviceWrapper.AtividadeService.UpdateAccessAtividade(atividadeAcessoUsuario, Guid.Parse(HttpContext.Items["User"].ToString()));

                return Ok(new Result<object>("Acesso Atualizado Para a Atividade"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpDelete("share")]
        [Authorize]
        public IActionResult DeleteShareAtividade([FromBody] AtividadeAcessoUsuario atividadeAcessoUsuario)
        {
            try
            {
                _serviceWrapper.AtividadeService.DeleteAccessAtividade(atividadeAcessoUsuario, Guid.Parse(HttpContext.Items["User"].ToString()));

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
