using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace GraphqlSample.API.Services
{
    public class IsSuperUserRequirement : AuthorizationHandler<IsSuperUserRequirement>, IAuthorizationRequirement
    {
        private string _roleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsSuperUserRequirement requirement)
        {
            if (context.User.HasClaim(x => x.Type == _roleClaimType))
            {
                var role = context.User.Claims.FirstOrDefault(x => x.Type == _roleClaimType).Value;
                if (role == "superuser")
                {
                    context.Succeed(requirement);
                    return Task.FromResult(0);
                }
            }
            context.Fail();
            return Task.FromResult(0);
        }
    }
}