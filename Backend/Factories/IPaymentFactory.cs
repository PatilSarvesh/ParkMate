using Backend.Models;

namespace Backend.Facories
{
    public interface IPaymentFactory
    {
        public SlotBooking CalculatetAmount(SlotBooking slotBooking);
    }
}