using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UserRegistrationSystem.Core.Models.Exceptions;
using UserRegistrationSystem.Core.Models.Models;
using UserRegistrationSystem.Core.Models.Response;
using UserRegistrationSystem.Core.Models.Validation;
using UserRegistrationSystem.Core.Services;
using UserRegistrationSystem.Infrastructure.Mapping;

namespace UserRegistrationSystem.Management.App.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserRegistrationSystemController : ControllerBase
    {
        private readonly IService _service;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public UserRegistrationSystemController(IService service)
        {
            _service = service;
        }

        /// <summary>
        /// ლოგირება
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _service.LogIn(model);
            if (string.IsNullOrWhiteSpace(result))
                return Unauthorized();
            return Ok(new
            {
                token = result
            });
        }

        /// <summary>
        /// მომხმარებლის შექმნა
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] AddUserRequest request)
        {
            try
            {
                RequestValidator validator = new RequestValidator();
                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    Dictionary<string, string[]> errorsDict = validationResult.Errors.GroupBy(e => e.PropertyName).ToDictionary(k => k.Key, v => v.Select(e => e.ErrorMessage).ToArray());
                    return ValidationProblem(new ValidationProblemDetails(errorsDict));
                }

                var user = request.ToUserMap();

                var result = await _service.AddUser(user);

                return Ok(result);
            }
            catch (UserExistsException)
            {
                return StatusCode((int)HttpStatusCode.Conflict, $"User With Username: {request.UserName} Already Exists!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromBody] AddUserRequest request)
        {
            try
            {
                RequestValidator validator = new RequestValidator();
                var results = validator.Validate(request);

                var user = new User
                {
                    PersonalId = request.PersonalId,
                    UserName = request.UserName,
                    IsMarried = request.IsMarried,
                    IsActive = request.IsActive,
                    IsEmployed = request.IsEmployed,
                    Email = request.Email,
                    Salary = 100,
                    Address = request.Address
                };

                var result = await _service.UpdateUser(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
