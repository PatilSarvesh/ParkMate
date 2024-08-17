using Backend.Common;
using Backend.Models;
using static Backend.Common.Costants;

namespace Backend.Facories
{

    public class PaymentFactory : IPaymentFactory
    {
        public PaymentFactory()
        {

        }

       public  SlotBooking CalculatetAmount(SlotBooking slotBooking)
        {

            // Calculate total parked hours
            var parkedHours = ((int)(slotBooking.ParkingExit - slotBooking.ParkingEntry).TotalHours);
            
            int costPerHour = 0;
            
            switch (slotBooking.VechileType.ToLower())
            {
                case VechileType.Bike:
                    costPerHour = int.Parse(ParkingCost.BikePerHr);
                    break;
                case VechileType.Car:
                    costPerHour = int.Parse(ParkingCost.CarPerHr);
                    break;
                case VechileType.Bicycle:
                    costPerHour = int.Parse(ParkingCost.BicyclePerHr);
                    break;
                default:
                    throw new ArgumentException("Invalid vehicle type");
            }
             slotBooking.Amount = parkedHours * costPerHour;
             return slotBooking;
        }
        public async Task GetPaymentDetailsByBookigId(string bookingId)
        {

        }
    }
}