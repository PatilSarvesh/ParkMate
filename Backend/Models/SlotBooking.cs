namespace Backend.Models
{
    public class SlotBooking
    {
        public object Id { get; set; }
        public string? UserId { get; set; }
        public string? SlotId { get; set; }
        public string? VechileNumber { get; set; }
        public string? VechileType { get; set; }
        public string? BookingId    { get; set; }
        public int? Amount { get; set; }
        public DateTime ParkingEntry { get; set; }
        public DateTime ParkingExit { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt {get; set; }
        public DateTime UpdatedAt {get; set; }
    

    }
}