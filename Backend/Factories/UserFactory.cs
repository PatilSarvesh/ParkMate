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
        private readonly IEmailSender _emailSender;
        private readonly string _key;
        #endregion
        #region Ctor

        public UserFactory(IUserService userService,IConfiguration configuration, IEmailSender emailSender)
        {
            _userService = userService;
            
			_key = configuration.GetSection("JwtKey").ToString();
            _emailSender = emailSender;
        }
        #endregion

        #region Methods
        public async Task<User> CreateUserAsync(User user)
        { 
            var existingUser = await _userService.GetUserByEmail(user.email);
            if ( existingUser != null)
            {   
                await _emailSender.SendEmailAsync(user.email, "Test", "This is test email");
                existingUser.jwtToken = Authenticate(existingUser.email);
                return  existingUser;
            }

            user.UserId = Guid.NewGuid().ToString();
            user.CreatedAt = DateTime.Now;
            await _userService.RegisterUser(user);
            await _emailSender.SendEmailAsync(user.email, "Test", "This is test email");
            user.jwtToken = Authenticate(user.email);
            return  user;
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