using Entities.DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApi
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IServiceManager _manager;

        public AuthenticationController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            var result = _manager.AuthenticationService.Register(userForRegistrationDto);

            if(!result)
                return BadRequest();

            return StatusCode(201);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserForLoginDto userForLogin)
        {

            var result = _manager.AuthenticationService.Login(userForLogin);

            if (!result)
                return Unauthorized();

            Console.WriteLine("Login is succed");
            return StatusCode(201);
        }
    }
}
