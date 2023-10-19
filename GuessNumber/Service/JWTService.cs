using GuessNumber.Dto;
using GuessNumber.Entity;
using GuessNumber.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GuessNumber.Service
{
    public class JWTService
    {
        public readonly IUserRepository _userRepository;

        public JWTService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Registration(UserDTO userDto)
        {
            var byEmail = _userRepository.GetUserByEmail(userDto.Email);
            if (byEmail.Result is null)
            {
                string passwordHash
                    = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

                User user = new User();
                user.Name = userDto.Name;
                user.Password = passwordHash;
                user.Email = userDto.Email;

                _userRepository.CreateUser(user);
                return user;
            }
            else
            {
                return null;
            }
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("asfsafsasafjsafjksafksafsafsafsafasfasfafasfsafasfsafsafassaf"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(7),
                signingCredentials: creds,
                issuer: "http://localhost:5069/"
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public string Login(LoginDTO loginDto)
        {
            if (loginDto.Email != null)
            {
                var user = _userRepository.GetUserByEmail(loginDto.Email);

                var verify = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Result?.Password);
                if (verify)
                {
                    if (user.Result != null) return CreateToken(user.Result);
                }
                return "wrong";
            }

            return "wrong";
        }

    }
}
