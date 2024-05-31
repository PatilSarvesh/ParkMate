using Backend.Models;
using Backend.Services;

namespace Backend.Facories
{
    public class SlotBookingFactory : ISlotBookingFactory
    {
        private readonly ISlotBookingService _slotBookingService;

        public SlotBookingFactory(ISlotBookingService slotBookingService)
        {
            _slotBookingService = slotBookingService;
        }

        public async Task<SlotBooking> CreateSlotBooking(SlotBooking slotBooking)
        {
            slotBooking.CreatedAt = DateTime.Now;
            slotBooking.Status = true;
            slotBooking.UserId =  Guid.NewGuid().ToString();
            slotBooking.CreatedAt = DateTime.Now;
            slotBooking.BookingId = Guid.NewGuid().ToString();
            

            slotBooking = await _slotBookingService.BookSlot(slotBooking);
            var slot = await _slotBookingService.GetSlotById(slotBooking.SlotId);
            slot.isAvailable = false;
            await _slotBookingService.UpdateSlots(slot);

            return slotBooking;
        }

        public async Task<List<Slots>> GetAllSlotsAsync()
        {
            var slots = await _slotBookingService.GetAllSlots();
            return slots;
        }
    }       
}