using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using UserRegistrationSystem.Core.Models.Models;

namespace UserRegistrationSystem.Core.Services
{
    public interface IUsersService
    {
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task<IdentityResult> DeleteUser(string userName);
        Task<string> LogIn(LoginModel model);
    }
}
