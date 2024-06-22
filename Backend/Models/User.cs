using MongoDB.Bson;

namespace Backend.Models
{
    public class User 
    {
    public ObjectId Id { get; set; }
    public string name { get; set;}
    public string email { get; set;}
    public string picture { get; set;}
    public string token { get; set;}
    }
}