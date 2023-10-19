using GuessNumber.Entity;

namespace GuessNumber.Repository
{
    public interface IGameRepository
    {
        Task<int> CreateGame(Game game);
    }
}
