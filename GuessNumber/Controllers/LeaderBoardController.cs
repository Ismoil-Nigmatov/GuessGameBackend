using GuessNumber.Entity;
using GuessNumber.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuessNumber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderBoardController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public LeaderBoardController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult>Get()
        {
            return Ok(await _userRepository.GetLeaderBoard());
        }
    }
}
