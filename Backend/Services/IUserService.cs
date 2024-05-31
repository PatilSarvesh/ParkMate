using Backend.Models;

namespace Backend.Services
{
    public interface IUserService
    {
          public Task<User> RegisterUser(User user);
          public  Task<User> GetUserByEmail(string email);
    }
}