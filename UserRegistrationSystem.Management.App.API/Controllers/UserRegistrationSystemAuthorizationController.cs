using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UserRegistrationSystem.Core.Models.Exceptions;
using UserRegistrationSystem.Core.Models.Models;
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
    public class UserRegistrationSystemAuthorizationController : Controller
    {
        private readonly IUsersService _service;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public UserRegistrationSystemAuthorizationController(IUsersService service)
        {
            _service = service;
        }

        /// <summary>
        /// მომხმარებლის შექმნა
        /// </summary>
        /// <param name="request">
        /// PersonalId - მომხმარებლის პირადი ნომერი;
        /// IsMarried - არის თუ არა დაოჯახებული მომხმარებელი;
        /// IsEmployed - არის თუ არა დასაქმებული მომხმარებელი;
        /// Salary - ხელფასი;
        /// Password - მომხმარებლის პაროლი;
        /// UserName - მომხმარებლის სისტემური სახელი;
        /// Email - მომხმარებლის ელექტრონული ფოსტა;
        /// Country - ქვეყანა;
        /// City - ქალაქი;
        /// Street - ქუჩა;
        /// Building - საცხოვრებელი შენობა(კორპუსი);
        /// Apartment - ბინა;
        /// </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] AddUserRequest request)
        {
            try
            {
                var validationResult = new RegistrationRequestValidator().ValidateRequest(request);
                if (validationResult != null)
                    return ValidationProblem(validationResult);

                var result = await _service.AddUser(request.ToUserMap());

                return Ok(result);
            }
            catch (UserAlreadyExistsException)
            {
                return StatusCode((int)HttpStatusCode.Conflict, $"User With Username: '{request.UserName}' Already Exists!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        /// <summary>
        /// დალოგინება
        /// </summary>
        /// <param name="model">
        /// UserName - მომხმარებლის სისტემური სახელი;
        /// Password - მომხმარებლის პაროლი;
        /// </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var validationResult = new LoginModelValidator().ValidateRequest(model);
            if (validationResult != null)
                return ValidationProblem(validationResult);

            var result = await _service.LogIn(model);
            if (string.IsNullOrWhiteSpace(result))
                return Unauthorized();
            return Ok(new
            {
                token = result
            });
        }
    }
}
