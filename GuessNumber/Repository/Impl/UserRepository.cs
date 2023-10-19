using GuessNumber.Data;
using GuessNumber.Dto;
using GuessNumber.Entity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GuessNumber.Repository.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
           return (await _context.Users.FirstOrDefaultAsync(u => u.Email == email))!;
        }

        public async Task<User> GetUserById()
        {
            var result = String.Empty;
            if (_httpContextAccessor.HttpContext is not null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }


            return (await _context.Users.FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(result)))!;
        }

        public async Task UpdateUser(int userId, int gameId)
        {
            var async = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (async is not null)
            {
                if (async.Games is null)
                {
                    async.Games = new List<Game>();
                }

                async.Games.Add((await _context.Games.FirstOrDefaultAsync(g=> g.Id == gameId))!);
                _context.Entry(async).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetLeaderBoard()
        {
            // var sortedUsers = await _context.Users
            //     .Include(u=> u.Games)
            //     .OrderBy(u => u.Games.Where(g => g.Win).Min(g => g.Attempts))
            //     .ToListAsync();
            // return sortedUsers;

            var sortedUsers = await _context.Users
                .Include(u => u.Games)
                .OrderByDescending(u => u.Games.Count(g => g.Win)) // Sort by biggest win
                .ThenBy(u => u.Games.Where(g => g.Win).Min(g => g.Attempts)) // Then sort by smallest attempt among winners
                .ToListAsync();

            return sortedUsers;
        }
    }
}
