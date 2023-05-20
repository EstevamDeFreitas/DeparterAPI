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
    public class FuncionarioController : ControllerBase
    {
        protected readonly IServiceWrapper _serviceWrapper;
        private readonly IWebHostEnvironment hostEnvironment;

        public FuncionarioController(IServiceWrapper serviceWrapper, IWebHostEnvironment hostEnvironment)
        {
            _serviceWrapper = serviceWrapper;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetFuncionario([FromQuery]Guid id)
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

        [Route("account/my")]
        [HttpGet]
        [Authorize]
        public IActionResult GetFuncionarioLogged()
        {
            try
            {
                var funcionario = _serviceWrapper.FuncionarioService.GetFuncionario(Guid.Parse(HttpContext.Items["User"].ToString()));

                return Ok(new Result<FuncionarioDTO>("Funcionário Encontrado", funcionario));
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

        [HttpGet("all")]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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

        [HttpPost("upload-image/{funcionarioId}")]
        [Authorize]
        public async Task<IActionResult> UploadImage(Guid funcionarioId)
        {
            try
            {
                var funcionario =  _serviceWrapper.FuncionarioService.GetFuncionario(funcionarioId);
                if (funcionario == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    DeleteImage(funcionario.Imagem);
                    funcionario.Imagem = await SaveImage(file);
                }
                _serviceWrapper.FuncionarioService.UpdateFuncionario(funcionario);

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
