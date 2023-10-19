using GuessNumber.Dto;
using GuessNumber.Entity;

namespace GuessNumber.Repository
{
    public interface IUserRepository
    {
        Task CreateUser(User user);

        Task<User> GetUserByEmail(string email);

        Task<User> GetUserById();

        Task UpdateUser(int userId, int gameId);

        Task<List<User>> GetLeaderBoard(); 
    }
}
