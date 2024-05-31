using Backend.Models;
using Backend.Services;

namespace Backend.Facories
{
    public class UserFactory : IUserFactory
    {
        private readonly IUserService _userService;
        public UserFactory(IUserService userService){
            _userService = userService;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var u = await _userService.GetUserByEmail(user.email);
            if(u != null)
            {
                return u;
            }
             await _userService.RegisterUser(user);
            return user;
        }
    }
}