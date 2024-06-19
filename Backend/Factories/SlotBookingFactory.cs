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

        public SlotBookingFactory(ISlotBookingService slotBookingService)
        {
            _slotBookingService = slotBookingService;
        }

        public async Task<SlotBooking> CreateSlotBooking(SlotBooking slotBooking)
        {
            slotBooking.CreatedAt = DateTime.Now;
            slotBooking.Status = true;
            slotBooking.UserId =  Guid.NewGuid().ToString();
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

        public async Task ExitSlot(string bookingId){

        }

        public async  Task<SlotBooking> GetBookingDetailsByBookingId(string bookingId)
        {
                var res = await _slotBookingService.GetSlotByBookingId(bookingId);
                return res;
        }

        public async Task CalculatetAmount(SlotBooking slotBooking)
        {
            slotBooking.ParkingExit = DateTime.Now;

            // Calculate total parked hours
            var parkedHours = (slotBooking.ParkingExit - slotBooking.ParkingEntry).TotalHours;
            
            double costPerHour = 0;
            
            switch (slotBooking.VechileType)
            {
                case Costants.VechileType.Bike:
                    costPerHour = double.Parse(Costants.ParkingCost.BikePerHr);
                    break;
                case Costants.VechileType.Car:
                    costPerHour = double.Parse(Costants.ParkingCost.CarPerHr);
                    break;
                case Costants.VechileType.Bicycle:
                    costPerHour = double.Parse(Costants.ParkingCost.BicyclePerHr);
                    break;
                default:
                    throw new ArgumentException("Invalid vehicle type");
            }
             slotBooking.Amount = parkedHours * costPerHour;
        }
    }       
}