using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserRegistrationSystem.Core.Models.Exceptions;
using UserRegistrationSystem.Core.Models.Models;
using UserRegistrationSystem.Core.Repositories;
using UserRegistrationSystem.Core.Services;
using UserRegistrationSystem.Infrastructure.Mapping.DBModelsMapping;

namespace UserRegistrationSystem.Infrastructure.Services
{
    public class Service : IService
    {
        private readonly IRepository _repository;
        private readonly IConfiguration _configuration;

        public Service(IRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<User> AddUser(User user)
        {
            var existedUser = await _repository.GetUserByUserName(user.UserName);
            if (existedUser != null)
            {
                throw new UserExistsException();
            }
            var result = await _repository.AddUser(user);
            return result.user;
        }

        public async Task<User> UpdateUser(User user)
        {
            var result = await _repository.UpdateUser(user);
            return result.user;
        }

        public async Task<string> LogIn(LoginModel model)
        {
            if (await _repository.CheckUserPassword(model))
            {
                var userRoles = await _repository.GetUserRoles(model.UserName);
                var authClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Secret").Value));
                var token = new JwtSecurityToken(issuer: _configuration.GetSection("JWT:ValidIssuer").Value,
                                                 audience: _configuration.GetSection("JWT:ValidAudience").Value,
                                                 expires: DateTime.Now.AddSeconds(150),
                                                 claims: authClaims,
                                                 signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256));


                var result = new JwtSecurityTokenHandler().WriteToken(token);
                return result;
            }

            return string.Empty;
        }        
    }
}