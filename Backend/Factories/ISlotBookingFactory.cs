using Backend.Models;

namespace Backend.Facories
{
    public interface ISlotBookingFactory
    {
        public Task<SlotBooking> CreateSlotBooking(SlotBooking slotBooking);
        public  Task<List<Slots>> GetAllSlotsAsync();
         public   Task<SlotBooking> GetBookingDetailsByBookingId(string bookingId);
    }
    
}