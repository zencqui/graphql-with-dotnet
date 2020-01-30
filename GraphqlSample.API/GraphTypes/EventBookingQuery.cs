using GraphQL.Types;
using GraphqlSample.Services;

namespace GraphqlSample.GraphTypes
{
    public class EventBookingQuery : ObjectGraphType
    {
        public EventBookingQuery(ContextServiceLocator serviceLocator)
        {
            Field<ListGraphType<UserGraphType>>(
                "users", 
                resolve: context => serviceLocator.UserService.All());
        }
    }
}