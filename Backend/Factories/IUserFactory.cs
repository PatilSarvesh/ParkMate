using Backend.Models;

namespace Backend.Facories
{
    public interface IUserFactory
    {
         public  Task<User> CreateUserAsync(User user);
    }
}