using GraphQL.Types;
using GraphqlSample.API.Models;

namespace GraphqlSample.API.GraphTypes
{
    public class BookingInputGraphType : InputObjectGraphType<Booking>
    {
        public BookingInputGraphType()
        {
            Name = "bookingInput";
            Field<NonNullGraphType<StringGraphType>>("user");
            Field<NonNullGraphType<StringGraphType>>("event");
        }
    }
}