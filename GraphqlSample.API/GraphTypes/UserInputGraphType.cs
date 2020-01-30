using GraphQL.Types;

namespace GraphqlSample.API.GraphTypes
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