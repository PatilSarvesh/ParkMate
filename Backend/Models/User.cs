using MongoDB.Bson;

namespace Backend.Models
{
    public class User 
    {
    public ObjectId Id { get; set; }
    public string UserId { get; set; }
    public string name { get; set;}
    public string email { get; set;}
    public string picture { get; set;}
    public string token { get; set;}
    public string jwtToken { get; set; }
    public DateTime CreatedAt { get; set; }
    }
}