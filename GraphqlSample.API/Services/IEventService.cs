using System.Collections.Generic;
using System.Threading.Tasks;
using GraphqlSample.API.Models;

namespace GraphqlSample.API.Services
{
    public interface IEventService
    {
        Task<Event> CreateEvent(Event @event);
        Task<List<Event>> All();
        Task<Booking> BookEvent(Booking booking);
        Task<Event> FindById(string eventId);
        Task<List<Booking>> GetAllBookings();
    }
}