using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserRegistrationSystem.Infrastructure.RelationalDatabase.DBEntities;

namespace UserRegistrationSystem.Core.Repositories
{
    public interface IUsersRepository
    {
        Task<Models.Models.User> AddUser(Models.Models.User user);
        Task<User> GetUserByUserName(string userName);
        Task<Models.Models.User> UpdateUserAndAddress(Models.Models.User user);
        Task<bool> CheckUserPassword(Models.Models.LoginModel loginModel);
        Task<List<string>> GetUserRoles(string userName);
        Task<IdentityResult> DeleteUser(User user);
    }
}