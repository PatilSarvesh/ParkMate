using MongoDB.Bson;

namespace Backend.Models
{
    public class Slots 
    {
        public ObjectId Id { get; set; }
        public string slotId { get; set; }
        public bool isAvailable { get; set; }
        public string type { get; set; }
    }
}