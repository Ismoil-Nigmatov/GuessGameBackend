using GuessNumber.Data;
using GuessNumber.Entity;

namespace GuessNumber.Repository.Impl
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext _context;

        public GameRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateGame(Game game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();

            int gameId = game.Id;

            return gameId;
        }
    }
}
