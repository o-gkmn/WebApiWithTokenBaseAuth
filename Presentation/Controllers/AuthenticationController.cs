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
        public async Task<IActionResult> Register([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            var result = await _manager.AuthenticationService.Register(userForRegistrationDto);

            if (!result.Succeeded)
                return BadRequest();

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLogin)
        {
            var result = await _manager.AuthenticationService.Login(userForLogin);
            if (!result)
                return Unauthorized();

            var tokenDto = await _manager.AuthenticationService.CreateToken(populateExp: true);
            return Ok(tokenDto);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var tokenDtoReturn = await _manager.AuthenticationService.RefreshToken(tokenDto);
            return Ok(tokenDtoReturn);
        }

        [ServiceFilter(typeof(TokenAuthenticationFilter))]
        [HttpGet("console")]
        public IActionResult ConsoleWrite()
        {
            Console.WriteLine("Hello World");
            return Ok();
        }
    }
}
