using Backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Backend.Services
{
    public class SlotBookingService :ISlotBookingService
    {
        private readonly IMongoCollection<Slot> _slots;
        private readonly IMongoCollection<SlotBooking> _slotsBooking;

        public SlotBookingService(IOptions<MongoDBSettings> mongoDBSettings, IOptions<MongoCollections> collections)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _slots = database.GetCollection<Slot>(collections.Value.SlotsCollection);
            _slotsBooking = database.GetCollection<SlotBooking>(collections.Value.SlotBookingCollection);
        }

        public async Task<SlotBooking> BookSlot(SlotBooking slotBooking)
        {
            await _slotsBooking.InsertOneAsync(slotBooking);
            return slotBooking;
        }
        public async Task<SlotBooking> UpdateSlot(SlotBooking slotBooking)
        {
            var filter = Builders<SlotBooking>.Filter.Eq(s => s.SlotId, slotBooking.SlotId);
            var update = Builders<SlotBooking>.Update.Set(s => s.Status, slotBooking.Status)
                                                     .Set(s => s.ParkingExit, slotBooking.ParkingExit);
            await _slotsBooking.UpdateOneAsync(filter,update);
            return slotBooking;
        }

        public async Task<Slot> UpdateSlots(Slot slots)
        {
            var slot = await GetSlotById(slots.slotId);
            var filter = Builders<Slot>.Filter.Eq(s => s.slotId, slots.slotId);
            var update = Builders<Slot>.Update.Set(s => s.isAvailable, slots.isAvailable)
                                                .Set(s=>s.isSlotInUse, slots.isSlotInUse);
            await _slots.UpdateOneAsync(filter, update);
            return slots;
        }

        public async Task<List<Slot>> GetAllSlots()
        {
            var slots = await _slots.AsQueryable().ToListAsync();
            return slots;
        }

        public async Task<Slot> GetSlotById(string slotId)
        {
            var slot = await _slots.Find(x=> x.slotId == slotId).FirstOrDefaultAsync();
            return slot;
        }

        public async Task<SlotBooking> GetSlotByBookingId(string bookingId)
        {
            var res = await _slotsBooking.Find(x => x.BookingId == bookingId).FirstOrDefaultAsync();
            return res;
        }

    }

}