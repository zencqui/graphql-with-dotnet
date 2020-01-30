using GraphQL.Types;
using GraphqlSample.API.Models;

namespace GraphqlSample.API.GraphTypes
{
    public class UserGraphType : ObjectGraphType<User>
    {
        public UserGraphType()
        {
            Field(x => x.Id);
            Field(x => x.email);
            Field(x => x.password);
        }
    }
}