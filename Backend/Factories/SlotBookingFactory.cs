using Backend.Facories;
using Backend.Hubs;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Factories
{
    public class SlotBookingFactory : ISlotBookingFactory
    {
        private readonly ISlotBookingService _slotBookingService;
        private readonly IPaymentFactory _paymentFactory;
        private readonly IHubContext<SlotHub> _hubContext;
        private readonly IMemoryCache _cache;

        public SlotBookingFactory(ISlotBookingService slotBookingService, IPaymentFactory paymentFactory, IHubContext<SlotHub> hubContext, IMemoryCache cache)
        {
            _slotBookingService = slotBookingService;
            _paymentFactory = paymentFactory;
            _hubContext = hubContext;
            _cache = cache;
        }

        public async Task<SlotBooking> CreateSlotBooking(SlotBooking slotBooking)
        {
            slotBooking.Status = true;
            slotBooking.BookingId = Guid.NewGuid().ToString();

            slotBooking = await _slotBookingService.BookSlot(slotBooking);

            var slot = await _slotBookingService.GetSlotById(slotBooking.SlotId);

            slot.isAvailable = false;
            slot.isSlotInUse = true;  
            await _slotBookingService.UpdateSlots(slot);

            await GetAllSlotsAsync();

            return slotBooking;
        }

        public async Task<List<Slot>> GetAllSlotsAsync()
        {
            var slots = await _slotBookingService.GetAllSlots();
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", slots);

            return slots;
        }

        public async Task ExitSlot(SlotBooking slotBooking)
        {

            slotBooking.Status = false;
            await _slotBookingService.UpdateSlot(slotBooking);

            var slot = await _slotBookingService.GetSlotById(slotBooking.SlotId);
            slot.isAvailable = true;
            await _slotBookingService.UpdateSlots(slot);
            await GetAllSlotsAsync();
        }

        public async Task<SlotBooking> GetBookingDetailsByBookingId(string bookingId)
        {
            var res = await _slotBookingService.GetSlotByBookingId(bookingId);
            res.ParkingExit = DateTime.Now;
            res = _paymentFactory.CalculatetAmount(res);
            return res;
        }
        public async Task LockSpot(string slotId)
        {
            _cache.Set(slotId, true, TimeSpan.FromMinutes(1)); // Lock the slot for 5 minutes or until confirmed/canceled
            var slot = await _slotBookingService.GetSlotById(slotId);
            slot.isSlotInUse = true; // Temporarily make the slot unavailable
            await _slotBookingService.UpdateSlots(slot);
            await GetAllSlotsAsync();
        }

        public async Task UnlockSpot(string slotId, bool isBookingConfirmed)
        {
            if (isBookingConfirmed)
            {
                // Slot will remain unavailable, no changes needed.
            }
            else
            {
                _cache.Remove(slotId); // Remove the temporary lock
                var slot = await _slotBookingService.GetSlotById(slotId);
                slot.isSlotInUse = false; // Make the slot available again
                await _slotBookingService.UpdateSlots(slot);
                await GetAllSlotsAsync();
            }
        }

        private async Task BroadcastSlotUpdate()
        {
            var slots = await _slotBookingService.GetAllSlots();
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", slots);
        }

    }
}