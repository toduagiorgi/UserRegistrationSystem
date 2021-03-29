using FluentValidation;
using Microsoft.AspNetCore.Authorization;
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
    //[Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserRegistrationSystemManagerController : ControllerBase
    {
        private readonly IUsersService _service;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public UserRegistrationSystemManagerController(IUsersService service)
        {
            _service = service;
        }

        /// <summary>
        /// მომხმარებელზე ინფორმაციის განახლება
        /// </summary>
        /// <param name="request">
        /// UserName - მომხმარებლის სისტემური სახელი;
        /// IsMarried - არის თუ არა დაოჯახებული მომხმარებელი;
        /// IsEmployed - არის თუ არა დასაქმებული მომხმარებელი;
        /// Salary - ხელფასი;        
        /// Country - ქვეყანა;
        /// City - ქალაქი;
        /// Street - ქუჩა;
        /// Building - საცხოვრებელი შენობა(კორპუსი);
        /// Apartment - ბინა;
        /// </param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateRequest request)
        {
            try
            {
                
                var validationResult = new UpdateRequestValidator().ValidateRequest(request);
                if (validationResult != null)
                    return ValidationProblem(validationResult);

                var user = request.ToUserMap();

                var result = await _service.UpdateUser(user);
                return Ok(result);
            }
            catch (UserNotExistsException)
            {
                return StatusCode((int)HttpStatusCode.NotFound, $"User With Username: '{request.UserName}' Not Exists!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        /// <summary>
        /// მომხმარებლის წაშლა
        /// </summary>
        /// <param name="request">
        /// UserName - მომხმარებლის სისტემური სახელი;
        /// </param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromQuery] DeleteUserRequest request)
        {
            try
            {
                var validationResult = new DeleteUserRequestValidator().ValidateRequest(request);
                if (validationResult != null)
                    return ValidationProblem(validationResult);

                var result = await _service.DeleteUser(request.UserName);
                return Ok(result);
            }
            catch (UserNotExistsException)
            {
                return StatusCode((int)HttpStatusCode.NotFound, $"User With Username: '{request.UserName}' Not Exists!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}