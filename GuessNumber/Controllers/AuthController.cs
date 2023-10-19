using GuessNumber.Dto;
using GuessNumber.Entity;
using Microsoft.AspNetCore.Mvc;
using GuessNumber.Service;

namespace GuessNumber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JWTService _jwtService;

        public AuthController(JWTService jwtService)
        {
            _jwtService = jwtService;
        }


        [HttpPost("register")]
        public ActionResult<User> Register(UserDTO request)
        {
            User registeredUser = _jwtService.Registration(request);
            if (registeredUser is not null)
            {
                return Ok(registeredUser);
            }
            else
            {
                return BadRequest("User with this email already exists.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO request)
        {
            try
            {
                return Ok(_jwtService.Login(request));
            }
            catch (Exception e)
            {
                return BadRequest("Incorrect User or Password");
            }
        }
    }
}
