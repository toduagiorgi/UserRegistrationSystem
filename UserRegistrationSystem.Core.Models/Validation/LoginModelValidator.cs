using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using UserRegistrationSystem.Core.Models.Models;

namespace UserRegistrationSystem.Core.Models.Validation
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(a => a.UserName).NotNull().NotEmpty()
                                    .WithMessage("მომხმარებლის სახელი არ შეიძლება იყოს ცარიელი!");
            RuleFor(a => a.Password).NotNull().NotEmpty()
                                    .WithMessage("მომხმარებლის პაროლი არ შეიძლება იყოს ცარიელი!");
        }
    }
}
