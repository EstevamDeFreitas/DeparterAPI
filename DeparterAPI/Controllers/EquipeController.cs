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
    public class EquipeController : ControllerBase
    {
        protected readonly IServiceWrapper _serviceWrapper;
        private readonly IWebHostEnvironment hostEnvironment;

        public EquipeController(IServiceWrapper serviceWrapper, IWebHostEnvironment hostEnvironment)
        {
            _serviceWrapper = serviceWrapper;
             this.hostEnvironment = hostEnvironment;
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetEquipe(Guid id, [FromQuery]bool? isAdminSearch)
        {
            try
            {
                var equipe = new EquipeDTO();

                if(isAdminSearch.HasValue && isAdminSearch == true)
                {
                    equipe = _serviceWrapper.EquipeService.GetEquipe(id);
                }
                else
                {
                    equipe = _serviceWrapper.EquipeService.GetEquipe(id, Guid.Parse(HttpContext.Items["User"].ToString()));
                }

                return Ok(new Result<EquipeDTO>("Equipe Encontrado", equipe));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetEquipes([FromQuery] bool? isAdminSearch)
        {
            try
            {
                var equipes = _serviceWrapper.EquipeService.GetEquipeList(isAdminSearch, Guid.Parse(HttpContext.Items["User"].ToString()));

                return Ok(new Result<List<EquipeDTO>>("Equipes Encontrados", equipes));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateEquipe([FromBody] EquipeCreateDTO equipe)
        {
            try
            {
                _serviceWrapper.EquipeService.CreateEquipe(equipe, Guid.Parse(HttpContext.Items["User"].ToString()));

                return Ok(new Result<object>("Equipe Criado"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpPut]
        [Authorize]
        public IActionResult UpdateEquipe([FromBody] EquipeDTO equipe)
        {
            try
            {
                _serviceWrapper.EquipeService.UpdateEquipe(equipe);

                return Ok(new Result<object>("Equipe alterado"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteEquipe(Guid id)
        {
            try
            {
                _serviceWrapper.EquipeService.DeleteEquipe(id);

                return Ok(new Result<object>("Equipe Deletado"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpPost("usuario")]
        [Authorize]
        public IActionResult AddUsuarioEquipe([FromQuery] Guid equipeId, [FromBody] List<Guid> usuarioId)
        {
            try
            {
                _serviceWrapper.EquipeService.AddUsuarioEquipe(equipeId, usuarioId);

                return Ok(new Result<object>("Usuario adicionado ao Equipe"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpPost("usuario/delete")]
        [Authorize]
        public IActionResult RemoveUsuarioEquipe([FromQuery] Guid equipeId, [FromBody] List<Guid> usuarioId)
        {
            try
            {
                _serviceWrapper.EquipeService.RemoveUsuarioEquipe(equipeId, usuarioId, Guid.Parse(HttpContext.Items["User"].ToString()));

                return Ok(new Result<object>("Usuario Removido do Equipe"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpGet("{id}/atividades/resumo")]
        [Authorize]
        public IActionResult GetEquipeAtividadesResumo(Guid id)
        {
            var atividades = _serviceWrapper.EquipeService.GetEquipeAtividadesResumo(id);

            return Ok(new Result<List<EquipeAtividadesResumoDTO>>("Resumo das atividades encontrado", atividades));
        }

        [HttpPost("upload-image/{equipeId}")]
        [Authorize]
        public async Task<IActionResult> UploadImage(Guid equipeId)
        {
            try
            {
                var equipe = _serviceWrapper.EquipeService.GetEquipe(equipeId);
                if (equipe == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    DeleteImage(equipe.ImageUrl);
                    equipe.ImageUrl = await SaveImage(file);
                }
                _serviceWrapper.EquipeService.UpdateEquipe(equipe);

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
