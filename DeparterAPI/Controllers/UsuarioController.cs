using DeparterAPI.Utilities;
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
    public class UsuarioController : ControllerBase
    {
        protected readonly IServiceWrapper _serviceWrapper;
        private readonly IWebHostEnvironment hostEnvironment;

        public UsuarioController(IServiceWrapper serviceWrapper, IWebHostEnvironment hostEnvironment)
        {
            _serviceWrapper = serviceWrapper;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUsuario([FromQuery]Guid id)
        {
            try
            {
                var usuario = _serviceWrapper.UsuarioService.GetUsuario(id);

                return Ok(new Result<UsuarioDTO>("Usuário Encontrado", usuario));
            }
            catch(Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [Route("account/my")]
        [HttpGet]
        [Authorize]
        public IActionResult GetUsuarioLogged()
        {
            try
            {
                var usuario = _serviceWrapper.UsuarioService.GetUsuario(Guid.Parse(HttpContext.Items["User"].ToString()));

                return Ok(new Result<UsuarioDTO>("Usuário Encontrado", usuario));
            }
            catch(Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            try
            {
                var token = _serviceWrapper.LoginService.Login(login);

                return Ok(new Result<string>("Login Realizado", token));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpPost]
        public IActionResult CreateUsuario([FromBody]UsuarioDTO usuario)
        {
            try
            {
                _serviceWrapper.UsuarioService.CreateUsuario(usuario);

                return Ok(new Result<object>("Usuário Criado"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpGet("all")]
        [Authorize]
        public IActionResult GetUsuarios()
        {
            try
            {
                var usuarios = _serviceWrapper.UsuarioService.GetUsuarios();

                return Ok(new Result<List<UsuarioDTO>>("Usuários Encontrado", usuarios));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpPut]
        [Authorize]
        public IActionResult UpdateUsuario([FromBody]UsuarioDTO usuario)
        {
            try
            {
                _serviceWrapper.UsuarioService.UpdateUsuario(usuario);

                return Ok(new Result<object>("Usuário Editado"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [Route("{id}")]
        [HttpDelete]
        [Authorize]
        public IActionResult DeleteUsuario(Guid id)
        {
            try
            {
                _serviceWrapper.UsuarioService.DeleteUsuario(id);

                return Ok(new Result<object>("Usuário Deletado"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>(ex.Message));
            }
        }

        [HttpPost("upload-image/{usuarioId}")]
        [Authorize]
        public async Task<IActionResult> UploadImage(Guid usuarioId)
        {
            try
            {
                var usuario =  _serviceWrapper.UsuarioService.GetUsuario(usuarioId);
                if (usuario == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    DeleteImage(usuario.Imagem);
                    usuario.Imagem = await SaveImage(file);
                }
                _serviceWrapper.UsuarioService.UpdateUsuario(usuario);

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
