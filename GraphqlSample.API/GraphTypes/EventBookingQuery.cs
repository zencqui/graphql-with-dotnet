using GraphQL.Server.Authorization.AspNetCore;
using GraphQL.Types;
using GraphqlSample.API.Models;
using GraphqlSample.API.Services;

namespace GraphqlSample.API.GraphTypes
{
    public class EventBookingQuery : ObjectGraphType
    {
        public EventBookingQuery(ContextServiceLocator serviceLocator)
        {
            this.AuthorizeWith("IsAdmin");
            Field<ListGraphType<UserGraphType>>(
                "users", 
                resolve: context => serviceLocator.UserService.All());//.AuthorizeWith("IsAdmin");

            Field<AuthDataGraphType>(
                "login",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LoginInputGraphType>> {Name = "login"}),
                resolve: context =>
                {
                    var cred = context.GetArgument<LoginCredential>("login");
                    return serviceLocator.UserService.Login(cred.email, cred.password);
                });

            Field<ListGraphType<EventGraphType>>(
                "events",
                resolve: context => serviceLocator.EventService.All());
            
            Field<ListGraphType<BookingGraphType>>(
                "bookings",
                resolve: context => serviceLocator.EventService.GetAllBookings());
        }
    }
}