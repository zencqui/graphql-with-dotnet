using GraphQL.Types;
using GraphqlSample.Models;

namespace GraphqlSample.GraphTypes
{
    public class UserInputGraphType : InputObjectGraphType
    {
        public UserInputGraphType()
        {
            Name = "userInput";
            Field<NonNullGraphType<StringGraphType>> ("email");
            Field<NonNullGraphType<StringGraphType>>("password");
        }
    }
}