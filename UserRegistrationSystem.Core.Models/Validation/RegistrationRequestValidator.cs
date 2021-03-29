using FluentValidation;
using UserRegistrationSystem.Core.Models.Models;

namespace UserRegistrationSystem.Core.Models.Validation
{
    public class RegistrationRequestValidator : AbstractValidator<AddUserRequest>
    {
        public RegistrationRequestValidator()
        {
            RuleFor(r => r.UserName).NotNull().WithMessage("მომხმარებლის სახელი არ შეიძლება იყოს ცარიელი!")
                                    .MinimumLength(6).WithMessage("მომხმარებლის სახელი უნდა შედგებოდეს 6 ან მეტი სიმბოლოსგან!");
            RuleFor(r => r.PersonalId).NotNull().WithMessage("პირადი ნომერი არ შეიძლება იყოს ცარიელი!")
                                      .Length(11).WithMessage("პირადი ნომერი უნდა შედგებოდეს 11 ციფრისგან")
                                      .Must(p => long.TryParse(p, out long n)).WithMessage("პირადი ნომერი უნდა შეიცავდეს მხოლოდ ციფრებს!");
            RuleFor(r => r.Salary).NotNull().GreaterThan(0).Unless(r => !r.IsEmployed).WithMessage("მიუთითეთ ხელფასის ოდენობა");
            RuleFor(r => r.IsMarried).NotNull().NotEmpty().WithMessage("ველი ოჯახური სტატუსი არ შეიძლება იყოს ცარიელი!");
            //RuleFor(r => r.IsEmployed).NotNull().NotEmpty().WithMessage("ველი სამსახურეობრივი მდგომარეობა არ შეიძლება იყოს ცარიელი!");

            RuleFor(r => r.Address.Country).NotNull().NotEmpty().WithMessage("ველი ქვეყანა არ შეიძლება იყოს ცარიელი!");
            RuleFor(r => r.Address.City).NotNull().NotEmpty().WithMessage("ველი ქალაქი არ შეიძლება იყოს ცარიელი!");
            RuleFor(r => r.Address.Street).NotNull().NotEmpty().WithMessage("ველი ქუჩა არ შეიძლება იყოს ცარიელი!");
            RuleFor(r => r.Address.Building).NotNull().NotEmpty().WithMessage("ველი შენობა არ შეიძლება იყოს ცარიელი!");
        }
    }
}