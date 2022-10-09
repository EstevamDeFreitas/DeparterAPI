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
    public class CategoriaController : ControllerBase
    {
        protected readonly IServiceWrapper _serviceWrapper;

        public CategoriaController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateCategoria([FromBody]CategoriaDTO categoria)
        {
            try
            {
                _serviceWrapper.CategoriaService.CreateCategoria(categoria);

                return Ok(new Result<object>("Categoria Criada"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetCategorias()
        {
            try
            {
                var categorias = _serviceWrapper.CategoriaService.GetCategorias();

                return Ok(new Result<List<CategoriaDTO>>("Categorias Encontradas", categorias));
            }
            catch(Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetCategoria(Guid id)
        {
            try
            {
                var categoria = _serviceWrapper.CategoriaService.GetCategoria(id);

                return Ok(new Result<CategoriaDTO>("Categoria Encontrada", categoria));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpPut]
        [Authorize]
        public IActionResult UpdateCategoria([FromBody] CategoriaDTO categoria)
        {
            try
            {
                _serviceWrapper.CategoriaService.UpdateCategoria(categoria);

                return Ok(new Result<object>("Categoria Atualizada"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteCategoria(Guid id)
        {
            try
            {
                _serviceWrapper.CategoriaService.DeleteCategoria(id);

                return Ok(new Result<object>("Categoria Deletada"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }
    }
}
