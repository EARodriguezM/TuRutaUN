using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TuRutaUN.Helpers;
using TuRutaUN.Models.User;
using TuRutaUN.Servicies;

namespace TuRutaUN.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        //Do global the interface of data user service
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromForm] Models.LoginUser.AuthenticateRequest authenticateRequest)
        {
            try
            {
                var user = await _userService.Authenticate(authenticateRequest);
                return Ok(user);
            }
            catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]RegisterRequest registerRequest)
        {
            try
            {
                await _userService.Register(registerRequest);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message });
            }
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users =  await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{dataUserId}")]
        public IActionResult GetById(string dataUserId)
        {
            // Only allow admins to access other user records
            var currentUserId = (User.Identity.Name).ToString();
            
            if (dataUserId != currentUserId || !User.IsInRole("1"))
                return Forbid();

            var user =  _userService.GetById(dataUserId);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("{dataUserId}")]
        public IActionResult Update([FromForm]UpdateRequest updateRequest)
        {
            
            // Only allow admins to access other user records
            var currentUserId = (User.Identity.Name).ToString();
            
            if (updateRequest.DataUserId != currentUserId || !User.IsInRole("1"))
                return Forbid();

            try
            {
                // Update user 
                _userService.Update(updateRequest);
                return Ok();
            }
            catch (AppException ex)
            {
                // Return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{dataUserId}")]
        public async Task<IActionResult> Delete(string dataUserId)
        {
            try
            {
                await _userService.Delete(dataUserId);
                return Ok();
            }
            catch (AppException ex)
            {
                // Return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}