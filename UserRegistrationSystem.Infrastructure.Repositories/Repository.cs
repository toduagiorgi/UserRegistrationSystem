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
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<RelationalDatabase.DBEntities.User> _userManager;
        public Repository(ApplicationDbContext context, UserManager<RelationalDatabase.DBEntities.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<(IdentityResult identityResult, User user)> AddUser(User user)
        {
            var dbUser = user.ToDbObject();
            var result = await _userManager.CreateAsync(dbUser, user.Password);
            return (result, dbUser.ToObject());
        }

        public async Task<(IdentityResult identityResult, User user)> UpdateUser(User user)
        {
            var dbUser = await GetUserByUserName(user.UserName);

            dbUser.IsMarried = user.IsMarried;
            dbUser.Salary = user.Salary;
            dbUser.IsEmployed = user.IsEmployed;
            dbUser.PersonalId = user.PersonalId;

            if (dbUser.Address != null)
            {
                dbUser.Address.Building = user.Address.Building;
                dbUser.Address.Country = user.Address.Country;
                dbUser.Address.City = user.Address.City;
                dbUser.Address.Street = user.Address.Street;
            }

            var result = await _userManager.UpdateAsync(dbUser);
            return (result, dbUser.ToObject());
        }

        public async Task<RelationalDatabase.DBEntities.User> GetUserByUserName(string userName)
        {
            var existedUser = await _userManager.FindByNameAsync(userName);
            if (existedUser != null)
                existedUser.Address = await GetAddressByUserId(existedUser.Id);
            return existedUser;
        }        

        public async Task<bool> CheckUserPassword(LoginModel loginModel)
        {
            var user = await FindUserByName(loginModel.UserName);
            return (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password));
        }

        public async Task<List<string>> GetUserRoles(string userName)
        {
            var user = await FindUserByName(userName);
            return (await _userManager.GetRolesAsync(user)).ToList();
        }        

        public async Task<RelationalDatabase.DBEntities.Address> AddAddress(RelationalDatabase.DBEntities.Address address)
        {
            var result = await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<RelationalDatabase.DBEntities.Address> GetAddressByUserId(Guid? userId)
        {
            var entity = await _context.Addresses.FirstOrDefaultAsync(item => item.UserId == userId);
            return entity;
        }

        private async Task<RelationalDatabase.DBEntities.User> FindUserByName(string userName)
        {
            var result = await _userManager.FindByNameAsync(userName);
            return result;
        }
    }
}