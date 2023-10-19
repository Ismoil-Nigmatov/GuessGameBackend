using GuessNumber.Entity;
using GuessNumber.Repository;
using GuessNumber.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace GuessNumber.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {

        private readonly IMemoryCache _memoryCache;
        private readonly IGameRepository _gameRepository;
        private readonly IUserRepository _userRepository;
        public GameController(IMemoryCache memoryCache, IGameRepository gameRepository, IUserRepository userRepository)
        {
            _memoryCache = memoryCache;
            _gameRepository = gameRepository;
            _userRepository = userRepository;
        }

        [HttpGet] 
        public IActionResult Get()
        {
            string sessionId = Guid.NewGuid().ToString();
            var gameSession = new GameSession();

            _memoryCache.Set(sessionId, gameSession);
            Game game = new Game();
            game.Attempts = 0;
            _memoryCache.Set("game", game);
            return Ok($"Welcome to the Number Guessing Game! Guess a number. You have 8 attempts. Your session ID is: {sessionId}");
        }

        [HttpPost("guess/{sessionId}")]
        public async Task<IActionResult> MakeGuess(string sessionId, string userInput)
        {
            Game game = _memoryCache.Get<Game>("game")!;
            if (_memoryCache.TryGetValue(sessionId, out var gameSession) && gameSession is GameSession session)
            {
                int[] userGuess;

                if (Logic.TryParseInput(userInput, out userGuess))
                {
                    foreach (int digit in session.SecretNumber)
                    {
                        Console.Write(digit + " ");
                    }
                    if (userGuess.SequenceEqual(session.SecretNumber))
                    {
                        session.Description.Add(userInput + " => " + "Congratulations! You've guessed the number.");
                        game.Attempts++;
                        _memoryCache.Remove(sessionId);
                        game.Win = true;
                        game.Description = session.Description;
                        int id = await _gameRepository.CreateGame(game);
                        var user = await _userRepository.GetUserById();
                        await _userRepository.UpdateUser(user.Id, id);
                        return Ok("Congratulations! You've guessed the number.");
                    }
                    else
                    {
                        session.Attempts--;
                        game.Attempts++;
                        int m = Logic.CalculateM(session.SecretNumber, userGuess);
                        int p = Logic.CalculateP(session.SecretNumber, userGuess);
                        m = m - p;

                        if (session.Attempts == 0)
                        {
                            session.Description.Add(userInput + " => " + $"Sorry, you didn't guess the number. The secret number was {string.Join("", session.SecretNumber)}.");
                            _memoryCache.Remove(sessionId);
                            game.Win = false;
                            game.Description = session.Description;
                            int id = await _gameRepository.CreateGame(game);
                            var user = await _userRepository.GetUserById();
                            await _userRepository.UpdateUser(user.Id, id);
                            return Ok($"Sorry, you didn't guess the number. The secret number was {string.Join("", session.SecretNumber)}.");
                        }
                        session.Description.Add(userInput + " => " + $"Incorrect guess. M: {m}, P: {p}. You have {session.Attempts} attempts left.");
                        return Ok($"Incorrect guess. M: {m}, P: {p}. You have {session.Attempts} attempts left.");
                    }
                }
                else
                {
                    return BadRequest("Invalid input. Please enter a 4-digit number.");
                }
            }
            else
            {
                return NotFound("Invalid or expired session ID.");
            }
        }
    }
}
