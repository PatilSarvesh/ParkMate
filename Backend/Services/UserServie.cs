using Backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Backend.Services
{
public class UserService: IUserService
{
    private readonly IMongoCollection<User> _users;

    public UserService(IOptions<MongoDBSettings> mongoDBSettings,IOptions<MongoCollections> collections)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _users = database.GetCollection<User>(collections.Value.UserCollection);
    }

    public async Task<User> RegisterUser(User user)
    {
        // Implement user registration logic
        await _users.InsertOneAsync(user);
        return user;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        var user = await _users.Find(u =>u.email == email).FirstOrDefaultAsync();
        return user;
    }

}
}