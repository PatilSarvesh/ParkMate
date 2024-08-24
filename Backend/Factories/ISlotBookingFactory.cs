using Backend.Models;

namespace Backend.Facories
{
    public interface ISlotBookingFactory
    {
        public Task<SlotBooking> CreateSlotBooking(SlotBooking slotBooking);
        public Task<List<Slot>> GetAllSlotsAsync();
        public Task<SlotBooking> GetBookingDetailsByBookingId(string bookingId);
        public Task ExitSlot(SlotBooking slotBooking);
        public Task LockSpot(string slotId);
        public Task UnlockSpot(string slotId, bool isBookingConfirmed);
    }

}