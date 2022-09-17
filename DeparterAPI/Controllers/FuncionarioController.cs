using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Services.Interfaces;
using Services.Utilities;

namespace DeparterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly IServiceWrapper _serviceWrapper;

        public FuncionarioController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetFuncionario(Guid id)
        {
            try
            {
                var funcionario = _serviceWrapper.FuncionarioService.GetFuncionario(id);

                return Ok(new Result<FuncionarioDTO>("Funcionário Encontrado", funcionario));
            }
            catch(Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpPost]
        public IActionResult CreateFuncionario([FromBody]FuncionarioDTO funcionario)
        {
            try
            {
                _serviceWrapper.FuncionarioService.CreateFuncionario(funcionario);

                return Ok(new Result<object>("Funcionário Criado"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpGet]
        public IActionResult GetFuncionarios()
        {
            try
            {
                var funcionarios = _serviceWrapper.FuncionarioService.GetFuncionarios();

                return Ok(new Result<List<FuncionarioDTO>>("Funcionários Encontrado", funcionarios));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpPut]
        public IActionResult UpdateFuncionario([FromBody]FuncionarioDTO funcionario)
        {
            try
            {
                _serviceWrapper.FuncionarioService.UpdateFuncionario(funcionario);

                return Ok(new Result<object>("Funcionário Editado"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult DeleteFuncionario(Guid id)
        {
            try
            {
                _serviceWrapper.FuncionarioService.DeleteFuncionario(id);

                return Ok(new Result<object>("Funcionário Deletado"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }
    }
}
