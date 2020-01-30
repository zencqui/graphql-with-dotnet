using System.Collections.Generic;
using System.Threading.Tasks;
using GraphqlSample.API.Models;

namespace GraphqlSample.API.Services
{
    public interface IUserService
    {
        Task<List<User>> All();
        Task<User> Add(User user);
        Task<AuthData> Login(string email, string password);
        Task<User> FindById(string userId);
    }
}