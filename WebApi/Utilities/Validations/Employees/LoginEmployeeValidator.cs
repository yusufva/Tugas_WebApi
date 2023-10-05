using FluentValidation;
using WebApi.DTOs.Employees;

namespace WebApi.Utilities.Validations.Employees
{
    public class LoginEmployeeValidator : AbstractValidator<EmployeeLoginDto>
    {
        public LoginEmployeeValidator()
        {
            RuleFor(e=>e.Email).NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(e=>e.Email).NotEmpty().MaximumLength(50);
        }
    }
}
