using Backend.Models;

namespace Backend.Facories
{
    public interface IPaymentFactory
    {
        public Task<SlotBooking> CalculatetAmount(SlotBooking slotBooking);
    }
}