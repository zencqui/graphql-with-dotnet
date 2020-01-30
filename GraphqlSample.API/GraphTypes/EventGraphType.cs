using GraphQL.Language.AST;
using GraphQL.Types;
using GraphqlSample.API.Models;
using GraphqlSample.API.Services;

namespace GraphqlSample.API.GraphTypes
{
    public class EventGraphType : ObjectGraphType<Event>
    {
        public EventGraphType(ContextServiceLocator serviceLocator)
        {
            Field(x => x.eventId);
            Field(x => x.title);
            Field(x => x.description);
            Field<DecimalGraphType>("price", resolve: context => context.Source.price);
            Field<DateGraphType>("date", resolve: context => context.Source.date);
            Field<UserGraphType>(
                "creator", 
                resolve: context => serviceLocator.UserService.FindById(context.Source.creatorId));
        }
    }
}