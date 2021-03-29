using System.Threading.Tasks;
using UserRegistrationSystem.Core.Repositories;
using UserRegistrationSystem.Infrastructure.RelationalDatabase;
using UserRegistrationSystem.Infrastructure.Mapping.DBModelsMapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using UserRegistrationSystem.Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UserRegistrationSystem.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<RelationalDatabase.DBEntities.User> _userManager;
        public UsersRepository(ApplicationDbContext context, UserManager<RelationalDatabase.DBEntities.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<User> AddUser(User user)
        {
            var dbUser = user.ToDbObject();
            await _userManager.CreateAsync(dbUser, user.Password);
            return dbUser.ToObject();
        }

        public async Task<IdentityResult> DeleteUser(RelationalDatabase.DBEntities.User user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result;
        }

        public async Task<User> UpdateUserAndAddress(User user)
        {
            var dbUser = await GetUserByUserName(user.UserName);
            if (dbUser != null)
            {
                dbUser.IsMarried = user.IsMarried;
                dbUser.IsEmployed = user.IsEmployed;
                dbUser.Salary = user.Salary;

                if (dbUser.Address != null)
                {
                    dbUser.Address.Building = user.Address.Building;
                    dbUser.Address.Country = user.Address.Country;
                    dbUser.Address.City = user.Address.City;
                    dbUser.Address.Street = user.Address.Street;
                    dbUser.Address.Apartment = user.Address.Apartment;
                }

                await _userManager.UpdateAsync(dbUser);
            }
            return dbUser.ToObject();
        }

        public async Task<RelationalDatabase.DBEntities.User> GetUserByUserName(string userName)
        {
            var existedUser = await FindUserByUserName(userName);
            if (existedUser != null)
            {
                existedUser.Address = await GetAddressByUserId(existedUser.Id);
            }
            return existedUser;
        }

        public async Task<bool> CheckUserPassword(LoginModel loginModel)
        {
            var user = await FindUserByUserName(loginModel.UserName);
            return (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password));
        }

        public async Task<List<string>> GetUserRoles(string userName)
        {
            var user = await FindUserByUserName(userName);
            return (await _userManager.GetRolesAsync(user)).ToList();
        }

        #region private methods
        private async Task<RelationalDatabase.DBEntities.Address> GetAddressByUserId(Guid? userId)
        {
            return await _context.Addresses.FirstOrDefaultAsync(item => item.UserId == userId);
        }

        private async Task<RelationalDatabase.DBEntities.User> FindUserByUserName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
        #endregion
    }
}