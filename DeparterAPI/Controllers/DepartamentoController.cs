using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Notations;
using Services.Services.Interfaces;
using Services.Utilities;

namespace DeparterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        protected readonly IServiceWrapper _serviceWrapper;

        public DepartamentoController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetDepartamento(Guid id)
        {
            try
            {
                var departamento = _serviceWrapper.DepartamentoService.GetDepartamento(id);

                return Ok(new Result<DepartamentoDTO>("Departamento Encontrado", departamento));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetDepartamentos()
        {
            try
            {
                var departamentos = _serviceWrapper.DepartamentoService.GetDepartamentoList();

                return Ok(new Result<List<DepartamentoDTO>>("Departamentos Encontrados", departamentos));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateDepartamento([FromBody] DepartamentoCreateDTO departamento)
        {
            try
            {
                _serviceWrapper.DepartamentoService.CreateDepartamento(departamento, Guid.Parse(HttpContext.Items["User"].ToString()));

                return Ok(new Result<object>("Departamento Criado"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }
    }
}
