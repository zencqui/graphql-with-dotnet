using System.Net.Http;
using GraphQL.Types;
using GraphqlSample.API.Models;
using GraphqlSample.API.Services;

namespace GraphqlSample.API.GraphTypes
{
    public class EventBookingMutation : ObjectGraphType<User>
    {
        public EventBookingMutation(ContextServiceLocator serviceLocator)
        {
            Field<UserGraphType>(
                "createUser",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UserInputGraphType>> {Name = "user"}),
                resolve: context =>
                {
                    var newUser = context.GetArgument<User>("user");
                    return serviceLocator.UserService.Add(newUser);
                });
            Field<EventGraphType>(
                "createEvent",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<EventInputGraphType>>{ Name = "event" }),
                resolve: context =>
                {
                    var newEvent = context.GetArgument<Event>("event");
                    return serviceLocator.EventService.CreateEvent(newEvent);
                });
        }
    }
}