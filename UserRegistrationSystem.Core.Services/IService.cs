using System.Threading.Tasks;
using UserRegistrationSystem.Core.Models.Models;
using UserRegistrationSystem.Core.Models.Response;

namespace UserRegistrationSystem.Core.Services
{
    public interface IService
    {
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task<string> LogIn(LoginModel model);
    }
}
