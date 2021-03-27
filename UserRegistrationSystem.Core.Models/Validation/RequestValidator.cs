using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using UserRegistrationSystem.Core.Models.Models;

namespace UserRegistrationSystem.Core.Models.Validation
{
    public class RequestValidator : AbstractValidator<AddUserRequest>
    {
        public RequestValidator()
        {
            RuleFor(x => x.PersonalId).NotEmpty().NotNull().Length(11);
            RuleFor(r => r.Salary).NotNull().GreaterThan(0).Unless(r => !r.IsEmployed).WithMessage("მონებს არ ვიღებთ!");
            //RuleFor(x => x.).NotEmpty().WithMessage("Please specify a first name");
            //RuleFor(x => x.Discount).NotEqual(0).When(x => x.HasDiscount);
            //RuleFor(x => x.Address).Length(20, 250);
            //RuleFor(x => x.Postcode).Must(BeAValidPostcode).WithMessage("Please specify a valid postcode");
        }
    }
}
