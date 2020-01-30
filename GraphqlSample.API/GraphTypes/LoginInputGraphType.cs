using GraphQL.Types;

namespace GraphqlSample.API.GraphTypes
{
    public class LoginInputGraphType : InputObjectGraphType
    {
        public LoginInputGraphType()
        {
            Name = "loginInput";
            Field<NonNullGraphType<StringGraphType>>("email");
            Field<NonNullGraphType<StringGraphType>>("password");
        }
    }
}