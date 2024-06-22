using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Models;
using Backend.Services;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Facories
{

    public class UserFactory : IUserFactory
    {
        #region Fields
        private readonly IUserService _userService;
        private readonly string _key;
        #endregion
        #region Ctor

        public UserFactory(IUserService userService,IConfiguration configuration)
        {
            _userService = userService;
            
			_key = configuration.GetSection("JwtKey").ToString();
        }
        #endregion

        #region Methods
        public async Task<string> CreateUserAsync(User user)
        {
            var u = await _userService.GetUserByEmail(user.email);
            if (u != null)
            {   

                return  Authenticate(u.email);
            }
            await _userService.RegisterUser(user);
            return  Authenticate(user.email);
        }

        private  string Authenticate(string email)
        
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email),
                }),

                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}