using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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
        private readonly IWebHostEnvironment hostEnvironment;

        public DepartamentoController(IServiceWrapper serviceWrapper, IWebHostEnvironment hostEnvironment)
        {
            _serviceWrapper = serviceWrapper;
             this.hostEnvironment = hostEnvironment;
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetDepartamento(Guid id, [FromQuery]bool? isAdminSearch)
        {
            try
            {
                var departamento = new DepartamentoDTO();

                if(isAdminSearch.HasValue && isAdminSearch == true)
                {
                    departamento = _serviceWrapper.DepartamentoService.GetDepartamento(id);
                }
                else
                {
                    departamento = _serviceWrapper.DepartamentoService.GetDepartamento(id, Guid.Parse(HttpContext.Items["User"].ToString()));
                }

                return Ok(new Result<DepartamentoDTO>("Departamento Encontrado", departamento));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetDepartamentos([FromQuery] bool? isAdminSearch)
        {
            try
            {
                var departamentos = _serviceWrapper.DepartamentoService.GetDepartamentoList(isAdminSearch, Guid.Parse(HttpContext.Items["User"].ToString()));

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

        [HttpPut]
        [Authorize]
        public IActionResult UpdateDepartamento([FromBody] DepartamentoDTO departamento)
        {
            try
            {
                _serviceWrapper.DepartamentoService.UpdateDepartamento(departamento);

                return Ok(new Result<object>("Departamento alterado"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteDepartamento(Guid id)
        {
            try
            {
                _serviceWrapper.DepartamentoService.DeleteDepartamento(id);

                return Ok(new Result<object>("Departamento Deletado"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpPost("funcionario")]
        [Authorize]
        public IActionResult AddFuncionarioDepartamento([FromQuery] Guid departamentoId, [FromBody] List<Guid> funcionarioId)
        {
            try
            {
                _serviceWrapper.DepartamentoService.AddFuncionarioDepartamento(departamentoId, funcionarioId);

                return Ok(new Result<object>("Funcionario adicionado ao Departamento"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpPost("funcionario/delete")]
        [Authorize]
        public IActionResult RemoveFuncionarioDepartamento([FromQuery] Guid departamentoId, [FromBody] List<Guid> funcionarioId)
        {
            try
            {
                _serviceWrapper.DepartamentoService.RemoveFuncionarioDepartamento(departamentoId, funcionarioId, Guid.Parse(HttpContext.Items["User"].ToString()));

                return Ok(new Result<object>("Funcionario Removido do Departamento"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpGet("{id}/atividades/resumo")]
        [Authorize]
        public IActionResult GetDepartamentoAtividadesResumo(Guid id)
        {
            var atividades = _serviceWrapper.DepartamentoService.GetDepartamentoAtividadesResumo(id);

            return Ok(new Result<List<DepartamentoAtividadesResumoDTO>>("Resumo das atividades encontrado", atividades));
        }

        [HttpPost("upload-image/{departamentoId}")]
        [Authorize]
        public async Task<IActionResult> UploadImage(Guid departamentoId)
        {
            try
            {
                var departamento = _serviceWrapper.DepartamentoService.GetDepartamento(departamentoId);
                if (departamento == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    DeleteImage(departamento.ImageUrl);
                    departamento.ImageUrl = await SaveImage(file);
                }
                _serviceWrapper.DepartamentoService.UpdateDepartamento(departamento);

                return Ok(new Result<object>("Imagem Alterada"));
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar adicionar eventos. Erro: {ex.Message}");
            }
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName)
                                            .Take(10)
                                            .ToArray()
                                        ).Replace(' ', '-');

            imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.FileName)}";

            var imagePath = Path.Combine(hostEnvironment.ContentRootPath, @"Resources/images", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            };

            return imageName;
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(hostEnvironment.ContentRootPath, @"Resources/images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        } 
    }
}
