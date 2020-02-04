using System.Collections.Generic;
using System.Security.Claims;

namespace GraphqlSample.API.Models
{
    public class GraphQLUserContext
    {
        public ClaimsPrincipal User { get; set; }
    }
}