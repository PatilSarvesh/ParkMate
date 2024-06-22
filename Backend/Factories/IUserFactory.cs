using Backend.Models;

namespace Backend.Facories
{
    public interface IUserFactory
    {
         public  Task<string> CreateUserAsync(User user);
    }
}