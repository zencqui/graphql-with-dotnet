using GraphQL.Types;
using GraphqlSample.Models;

namespace GraphqlSample.GraphTypes
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