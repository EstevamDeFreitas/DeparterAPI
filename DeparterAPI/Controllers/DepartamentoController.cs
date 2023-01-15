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
        public IActionResult CreateDepartamento([FromBody] DepartamentoDTO departamento)
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
