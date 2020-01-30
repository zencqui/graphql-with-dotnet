using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace GraphqlSample.Services
{
    public class ContextServiceLocator
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IUserService UserService =>
            _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IUserService>();
        public ContextServiceLocator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
    }
}