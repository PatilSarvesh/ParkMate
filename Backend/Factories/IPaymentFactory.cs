using Backend.Models;

namespace Backend.Factories
{
    public interface IPaymentFactory
    {
        public SlotBooking CalculatetAmount(SlotBooking slotBooking);
    }
}