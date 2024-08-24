using Backend.Models;

namespace Backend.Services
{
    public interface ISlotBookingService
    {
        public  Task<SlotBooking> BookSlot(SlotBooking slotBooking);
        public  Task<SlotBooking> UpdateSlot(SlotBooking slotBooking);
        public  Task<Slot> UpdateSlots(Slot slots);
         public  Task<Slot> GetSlotById(string slotId);
          public  Task<List<Slot>> GetAllSlots();
          public  Task<SlotBooking> GetSlotByBookingId(string bookingId);
    }
    
}