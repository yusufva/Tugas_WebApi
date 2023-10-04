using FluentValidation;
using WebApi.DTOs.Account;

namespace WebApi.Utilities.Validations.Accounts
{
    public class UpdateAccountsValidator : AbstractValidator<AccountsDto>
    {
        public UpdateAccountsValidator() {
            RuleFor(e => e.Guid).NotEmpty();
            RuleFor(e => e.Password).NotEmpty().MinimumLength(8).MaximumLength(50);
            RuleFor(e => e.Otp).NotNull();
            RuleFor(e=> e.IsUsed).NotNull();
        }
    }
}
