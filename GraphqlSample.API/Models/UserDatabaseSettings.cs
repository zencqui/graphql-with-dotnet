namespace GraphqlSample.API.Models
{
    public class EventBookingDatabaseSettings : IEventBookingDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string UserCollection { get; set; }
        public string EventCollection { get; set; }
    }

    public interface IEventBookingDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string UserCollection { get; set; }
        string EventCollection { get; set; }
    }
}