namespace GraphqlSample.API.Services
{
    public static class JwtClaimType
    {
        public static string Role
        {
            get { return "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"; }
        }
    }
}