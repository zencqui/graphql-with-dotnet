using GraphQL.Types;
using GraphqlSample.Models;
using GraphqlSample.Services;

namespace GraphqlSample.GraphTypes
{
    public class EventBookingMutation : ObjectGraphType<User>
    {
        public EventBookingMutation(ContextServiceLocator serviceLocator)
        {
            Field<UserGraphType>(
                "createUser",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UserInputGraphType>> { Name = "user"}),
                resolve: context =>
                {
                    var newUser = context.GetArgument<User>("user");
                    return serviceLocator.UserService.Add(newUser);
                });
        }
    }
}