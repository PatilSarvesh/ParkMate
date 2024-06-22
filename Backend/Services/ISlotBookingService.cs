using Backend.Models;

namespace Backend.Services
{
    public interface ISlotBookingService
    {
        public  Task<SlotBooking> BookSlot(SlotBooking slotBooking);
        public  Task<SlotBooking> UpdateSlot(SlotBooking slotBooking);
        public  Task<Slots> UpdateSlots(Slots slots);
         public  Task<Slots> GetSlotById(string slotId);
          public  Task<List<Slots>> GetAllSlots();
          public  Task<SlotBooking> GetSlotByBookingId(string bookingId);
    }
    
}