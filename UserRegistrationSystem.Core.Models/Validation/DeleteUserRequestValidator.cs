using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using UserRegistrationSystem.Core.Models.Models;

namespace UserRegistrationSystem.Core.Models.Validation
{
    public class DeleteUserRequestValidator : AbstractValidator<DeleteUserRequest>
    {
        public DeleteUserRequestValidator()
        {
            RuleFor(userName => userName.UserName).NotNull()
                                                  .NotEmpty()
                                                  .WithMessage("მომხმარებლის სახელი არ შეიძლება იყოს ცარიელი!")
                                                  .MinimumLength(6)
                                                  .WithMessage("მომხმარებლის სახელი უნდა შედგებოდეს 6 ან მეტი სიმბოლოსგან!");
        }
    }
}
