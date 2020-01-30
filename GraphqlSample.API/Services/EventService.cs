using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphqlSample.API.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GraphqlSample.API.Services
{
    public class EventService : IEventService
    {
        private readonly IMongoCollection<Event> _events;
        private readonly IMongoCollection<User> _user;

        public EventService(IEventBookingDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _events = database.GetCollection<Event>(settings.EventCollection);
            _user = database.GetCollection<User>(settings.UserCollection);
        }
        public async Task<Event> CreateEvent(Event @event)
        {
            if(@event == null) throw new ArgumentNullException(nameof(@event));

            @event.creatorId = "5e323ecc39460a70d8e8087b";
            await _events.InsertOneAsync(@event);

            return @event;
        }

        public async Task<List<Event>> All()
        {
            var events = await _events.Find(new BsonDocument()).ToListAsync();
            return events;
        }
    }
}