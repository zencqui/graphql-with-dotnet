using System.Collections.Generic;
using System.Threading.Tasks;
using GraphqlSample.Models;

namespace GraphqlSample.Services
{
    public interface IUserService
    {
        Task<List<User>> All();
        Task<User> Add(User user);
    }
}