namespace GraphqlSample.Models
{
    public class EventBookingDatabaseSettings : IEventBookingDatabaseSettings
    {
        public string UserCollection { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IEventBookingDatabaseSettings
    {
        string UserCollection { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}