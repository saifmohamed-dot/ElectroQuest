using ElectroQuest.Application.Users.DTO;
using ElectroQuest.Application.Users.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectroQuest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly UserLoginHandler _loginHandler;
        readonly UserRegisterHandler _registerHandler;
        public AuthController(UserLoginHandler handler , UserRegisterHandler regHandler)
        {
            _loginHandler = handler;
            _registerHandler = regHandler;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            var query = new UserLoginQuery(loginDto);
            var result = await _loginHandler.HandleAsync(query);
            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
            return Ok(result.Token);
        }
        [HttpPost("register")]
        public async Task<IActionResult> SignUp(UserRegisterDto registerDto)
        {
            var command = new UserRegisterCommand(registerDto);
            var result = await _registerHandler.HandleAsync(command);
            if(!result.Success)
            {
                return Conflict(result.ErrorMessage); // conflict with the duplicate email.
            }
            return Ok("Login Please");
        }
    }
}
