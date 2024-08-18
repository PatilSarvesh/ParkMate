
using Backend.Common;
using Backend.Models;


namespace Backend.Facories
{

    public class PaymentFactory : IPaymentFactory
    {
        public PaymentFactory()
        {

        }

        public SlotBooking CalculatetAmount(SlotBooking slotBooking)
        {

            // Calculate total parked hours
            var parkedHours = ((int)(slotBooking.ParkingExit - slotBooking.ParkingEntry).TotalHours);

            int cost = 0;

            switch (slotBooking.VechileType.ToLower())
            {
                case HelperConstants.VechileType.Bike:
                    cost = CalculateBikeAmmount(parkedHours);
                    break;
                case HelperConstants.VechileType.Car:
                    cost = CalculateCarAmmount(parkedHours);
                    break;
                case HelperConstants.VechileType.Bicycle:
                    cost = CalculateBicycleAmmount(parkedHours);
                    break;
                default:
                    throw new ArgumentException("Invalid vehicle type");
            }
            slotBooking.Amount = cost;
            return slotBooking;
        }
        public async Task GetPaymentDetailsByBookigId(string bookingId)
        {

        }
        private int CalculateBikeAmmount(int hours)
        {
            if (hours <= 2)
                return HelperConstants.ParkingCost.BikePerHr;
            else if (hours <= 4)
                return HelperConstants.ParkingCost.BikePerHr + 50;
            else if (hours <= 6)
                return HelperConstants.ParkingCost.BikePerHr + 100;
            else
                return UnitConversions.HoursToDays(hours) * HelperConstants.ParkingCost.BikePerDay;

        }
        private int CalculateCarAmmount(int hours)
        {
            if (hours <= 2)
                return HelperConstants.ParkingCost.CarPerHr;
            else if (hours <= 4)
                return HelperConstants.ParkingCost.CarPerHr + 50;
            else if (hours <= 6)
                return HelperConstants.ParkingCost.CarPerHr + 100;
            else
                return UnitConversions.HoursToDays(hours) * HelperConstants.ParkingCost.CarPerDay;
        }
        private int CalculateBicycleAmmount(int hours)
        {
            if (hours <= 2)
                return HelperConstants.ParkingCost.BicyclePerHr;
            else if (hours <= 4)
                return HelperConstants.ParkingCost.BicyclePerHr + 50;
            else if (hours <= 6)
                return HelperConstants.ParkingCost.BicyclePerHr + 100;
            else
                return UnitConversions.HoursToDays(hours) * HelperConstants.ParkingCost.BicyclePerDay;

        }
    }
}