namespace Backend.Models
{
    public class MongoDBSettings
    {
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
        public string? UserCollection { get; set;}
    }
}