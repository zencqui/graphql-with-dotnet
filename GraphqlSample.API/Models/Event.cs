using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GraphqlSample.API.Models
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string eventId { get; set; }
        public string title { get; set; }
        public string description { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal price { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime date { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string creatorId { get; set; }
    }
}