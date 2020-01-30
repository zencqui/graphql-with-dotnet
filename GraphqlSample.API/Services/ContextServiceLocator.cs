using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace GraphqlSample.API.Services
{
    public class ContextServiceLocator
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IUserService UserService =>
            _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IUserService>();
        public IEventService EventService =>
            _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IEventService>();
        public ContextServiceLocator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
    }
}