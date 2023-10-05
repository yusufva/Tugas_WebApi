using FluentValidation;
using WebApi.DTOs.Employees;

namespace WebApi.Utilities.Validations.Employees
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordRequestDto>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(e => e.Email).NotEmpty().EmailAddress().MaximumLength(100);
        }
    }
}
