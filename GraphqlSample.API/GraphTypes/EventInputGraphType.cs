using GraphQL.Types;

namespace GraphqlSample.API.GraphTypes
{
    public class EventInputGraphType : InputObjectGraphType
    {
        public EventInputGraphType()
        {
            Name = "eventInput";
            //Field<StringGraphType>("eventId");
            Field<NonNullGraphType<StringGraphType>>("title");
            Field<NonNullGraphType<StringGraphType>>("description");
            Field<NonNullGraphType<DecimalGraphType>>("price");
            Field<NonNullGraphType<DateGraphType>>("date");
        }
    }
}