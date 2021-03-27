using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserRegistrationSystem.Infrastructure.RelationalDatabase.DBEntities;

namespace UserRegistrationSystem.Core.Repositories
{
    public interface IRepository
    {
        Task<(IdentityResult identityResult, Models.Models.User user)> AddUser(Models.Models.User user);
        Task<Address> AddAddress(Address address);
        Task<User> GetUserByUserName(string userName);
        Task<Address> GetAddressByUserId(Guid? userId);
        Task<(IdentityResult identityResult, Models.Models.User user)> UpdateUser(Models.Models.User user);
        Task<bool> CheckUserPassword(Models.Models.LoginModel loginModel);
        Task<List<string>> GetUserRoles(string userName);
    }
}