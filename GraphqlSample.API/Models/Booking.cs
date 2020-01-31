using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GraphqlSample.API.Models
{
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string bookingId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string user { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string @event { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime createdAt { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime updatedAt { get; set; }
    }
}