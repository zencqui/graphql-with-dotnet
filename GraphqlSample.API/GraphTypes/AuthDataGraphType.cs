using GraphQL.Types;
using GraphqlSample.API.Models;

namespace GraphqlSample.API.GraphTypes
{
    public class AuthDataGraphType : ObjectGraphType<AuthData>
    {
        public AuthDataGraphType()
        {
            Field(x => x.UserId);
            Field(x => x.Token);
            Field<DateTimeGraphType>("Expiration",
                resolve: context => context.Source.Expiration.Value.ToLongDateString());
        }
    }
}