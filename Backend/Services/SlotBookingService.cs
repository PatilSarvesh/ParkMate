using Backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Backend.Services
{
    public class SlotBookingService :ISlotBookingService
    {
        private readonly IMongoCollection<Slots> _slots;
        private readonly IMongoCollection<SlotBooking> _slotsBooking;

        public SlotBookingService(IOptions<MongoDBSettings> mongoDBSettings, IOptions<MongoCollections> collections)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _slots = database.GetCollection<Slots>(collections.Value.SlotsCollection);
            _slotsBooking = database.GetCollection<SlotBooking>(collections.Value.SlotBookingCollection);
        }

        public async Task<SlotBooking> BookSlot(SlotBooking slotBooking)
        {
            await _slotsBooking.InsertOneAsync(slotBooking);
            return slotBooking;
        }

        public async Task<Slots> UpdateSlots(Slots slots)
        {
            var slot = await GetSlotById(slots.slotId);
            var filter = Builders<Slots>.Filter.Eq(s => s.slotId, slots.slotId);
            var update = Builders<Slots>.Update.Set(s => s.isAvailable, slots.isAvailable);
            await _slots.UpdateOneAsync(filter, update);
            return slots;
            
        }
        public async Task<List<Slots>> GetAllSlots()
        {
            var slots = await _slots.AsQueryable().ToListAsync();
            return slots;
        }

        public async Task<Slots> GetSlotById(string slotId)
        {
            var slot = await _slots.Find(x=> x.slotId == slotId).FirstOrDefaultAsync();
            return slot;
        }


    }

}