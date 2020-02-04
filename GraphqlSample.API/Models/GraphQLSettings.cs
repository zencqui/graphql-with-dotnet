using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Validation;
using Microsoft.AspNetCore.Http;

namespace GraphqlSample.API.Models
{
    public class GraphQLSettings
    {
        public Func<HttpContext, Task<GraphQLUserContext>> BuildUserContext { get; set; }
        public PathString Path { get; set; } = "/api/graphql";
        public List<IValidationRule> ValidationRules { get; } = new List<IValidationRule>();
    }
}