using GraphQL;
using GraphQL.Types;

namespace GraphqlSample.API.GraphTypes
{
    public class EventBookingSchema : Schema
    {
        public EventBookingSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<EventBookingQuery>();
            Mutation = resolver.Resolve<EventBookingMutation>();
        }
    }
}