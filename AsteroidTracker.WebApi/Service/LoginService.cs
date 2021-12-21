namespace AsteroidTracker.WebApi.Service;

using AsteroidTracker.ViewModel;
using AsteroidTracker.WebApi.Model;
using AsteroidTracker.WebApi.Utils;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class LoginService
    {
       

        private readonly Configuration _config;
        private readonly List<User> _users = new()
        {
            new User { Id = 1,  Username = "Admin@test.com", Password = "Admin@1234", IsAdmin = true },
            new User { Id = 2,  Username = "Client@test.com", Password = "Client@1234", IsAdmin = false },
        };

        public LoginService(IOptions<Configuration> config)
        {
            _config = config.Value;
        }

        public LoginResponse Authenticate(LoginRequest rq)
        {
            LoginResponse rs = new();
            var user = _users.SingleOrDefault(x => x.Username == rq.Username && x.Password == rq.Password);

            if (user == null) {
                rs.Success = false;
                return rs;
            }

            return new LoginResponse() { 
                Success = true,
                Token= GenerateToken(user) 
            };
        }

        public User GetById(int id)
        {
            return _users.Find(x => x.Id == id);
        }

    private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("id", user.Id.ToString()),
                    new Claim("username", user.Username.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }