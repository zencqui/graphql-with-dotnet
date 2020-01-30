using System.Collections.Generic;
using System.Threading.Tasks;
using GraphqlSample.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GraphqlSample.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;
        public UserService(IEventBookingDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UserCollection);
        }

        public Task<User> Add(User user)
        {
            _users.InsertOneAsync(user);
            return Task.FromResult(user);
        }

        public Task<List<User>> All()
        {
            return _users.Find(new BsonDocument()).ToListAsync(); 
        }
    }
}