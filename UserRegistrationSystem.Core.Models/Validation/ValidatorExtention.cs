using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace UserRegistrationSystem.Core.Models.Validation
{
    public static class ValidatorExtention
    {
        public static ValidationProblemDetails ValidateRequest<T>(this AbstractValidator<T> validator, T model)
        {
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errorsDict = validationResult.Errors.GroupBy(e => e.PropertyName).ToDictionary(k => k.Key, v => v.Select(e => e.ErrorMessage).ToArray());
                return new ValidationProblemDetails(errorsDict);
            }
            return null;
        }
    }
}
