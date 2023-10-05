using FluentValidation;
using WebApi.DTOs.Account;

namespace WebApi.Utilities.Validations.Accounts
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequestDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(e => e.Email).NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(e => e.Otp).NotEmpty();
            RuleFor(e => e.NewPassword).NotEmpty().MaximumLength(50);
            RuleFor(e => e.ConfirmPassword).NotEmpty().Equal(e=>e.NewPassword);

        }
    }
}
