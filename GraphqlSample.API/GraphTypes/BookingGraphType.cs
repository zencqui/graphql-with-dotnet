using GraphQL.Types;
using GraphqlSample.API.Models;
using GraphqlSample.API.Services;

namespace GraphqlSample.API.GraphTypes
{
    public class BookingGraphType : ObjectGraphType<Booking>
    {
        public BookingGraphType(ContextServiceLocator serviceLocator)
        {
            Field(x => x.bookingId);
            Field<UserGraphType>("user", resolve: context => serviceLocator.UserService.FindById(context.Source.user));
            Field<EventGraphType>("event",
                resolve: context => serviceLocator.EventService.FindById(context.Source.@event));
            Field<DateTimeGraphType>("createdAt", resolve: context => context.Source.createdAt);
            Field<DateTimeGraphType>("updatedAt", resolve: context => context.Source.updatedAt);
        }
    }
}