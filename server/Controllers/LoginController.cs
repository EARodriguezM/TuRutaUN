using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TuRutaUN.Helpers;
using TuRutaUN.Models.LoginUser;
using TuRutaUN.Servicies;

namespace TuRutaUN.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]RegisterRequest registerRequest)
        {
            try
            {
                await _loginService.Register(registerRequest);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message });
            }
        }
    }
}