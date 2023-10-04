using FluentValidation;
using WebApi.DTOs.Account;

namespace WebApi.Utilities.Validations.Accounts
{
    public class UpdateAccountsValidator : AbstractValidator<AccountsDto>
    {
        public UpdateAccountsValidator() {
            RuleFor(e => e.Guid).NotEmpty(); //rule validator guid
            RuleFor(e => e.Password).NotEmpty().Length(8,50); //rule validator password
            RuleFor(e => e.Otp).NotNull(); //rule validator otp
            RuleFor(e=> e.IsUsed).NotNull(); //rule validator is used
        }
    }
}
