using Backend.Models;

namespace Backend.Factories
{
    public interface IUserFactory
    {
         public  Task<User> CreateUserAsync(User user);
    }
}