using System;

namespace GraphqlSample.API.Models
{
    public class AuthData
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime? Expiration { get; set; }
    }
}