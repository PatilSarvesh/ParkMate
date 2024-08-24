using Backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Backend.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMongoCollection<Slot> _slots;
        private readonly IMongoCollection<SlotBooking> _slotsBooking;

        public PaymentService(IOptions<MongoDBSettings> mongoDBSettings, IOptions<MongoCollections> collections)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _slots = database.GetCollection<Slot>(collections.Value.SlotsCollection);
            _slotsBooking = database.GetCollection<SlotBooking>(collections.Value.SlotBookingCollection);
        }

        public async Task<SlotBooking> GetBookingDetailsByBookingId(string bookingId)
        {
             var res = await _slotsBooking.Find(x => x.BookingId == bookingId).FirstOrDefaultAsync();
            return res;
        }
        
    }
}