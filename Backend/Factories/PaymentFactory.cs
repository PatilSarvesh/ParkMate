using Backend.Common;
using Backend.Models;

namespace Backend.Facories
{

    public class PaymentFactory : IPaymentFactory
    {
        public PaymentFactory()
        {

        }

        // private  SlotBooking  CalculatetAmount(SlotBooking slotBooking)
        // {
        //     slotBooking.ParkingExit = DateTime.Now;

        //     // Calculate total parked hours
        //     var parkedHours = (slotBooking.ParkingExit - slotBooking.ParkingEntry).TotalHours.;
            
        //     double costPerHour = 0;
            
        //     switch (slotBooking.VechileType)
        //     {
        //         case Costants.VechileType.Bike:
        //             costPerHour = double.Parse(Costants.ParkingCost.BikePerHr);
        //             break;
        //         case Costants.VechileType.Car:
        //             costPerHour = double.Parse(Costants.ParkingCost.CarPerHr);
        //             break;
        //         case Costants.VechileType.Bicycle:
        //             costPerHour = double.Parse(Costants.ParkingCost.BicyclePerHr);
        //             break;
        //         default:
        //             throw new ArgumentException("Invalid vehicle type");
        //     }
        //      slotBooking.Amount = parkedHours * costPerHour;
        //      return slotBooking;
        // }

        public async Task GetPaymentDetailsByBookigId(string bookingId)
        {

        }
    }
}