using System.Diagnostics;
using Backend.Common;
using Backend.Models;
using Backend.Services;
using Microsoft.Extensions.Options;
using static Backend.Common.Costants;

namespace Backend.Facories
{
    public class SlotBookingFactory : ISlotBookingFactory
    {
        private readonly ISlotBookingService _slotBookingService;
        private readonly IPaymentFactory _paymentFactory;

        public SlotBookingFactory(ISlotBookingService slotBookingService, IPaymentFactory paymentFactory)
        {
            _slotBookingService = slotBookingService;
            _paymentFactory = paymentFactory;
        }

        public async Task<SlotBooking> CreateSlotBooking(SlotBooking slotBooking)
        {
            slotBooking.Status = true;
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

        public async Task ExitSlot(SlotBooking slotBooking)
        {
            
            slotBooking.Status = false;
            await _slotBookingService.UpdateSlot(slotBooking);

            var slot = await _slotBookingService.GetSlotById(slotBooking.SlotId);
            slot.isAvailable = true;
            await _slotBookingService.UpdateSlots(slot);
        }

        public async  Task<SlotBooking> GetBookingDetailsByBookingId(string bookingId)
        {
                var res = await _slotBookingService.GetSlotByBookingId(bookingId);
                    res.ParkingExit = DateTime.Now;
                    res = await _paymentFactory.CalculatetAmount(res);
                return res;
        } 

        
    }       
}