using FluentValidation;
using WebApi.DTOs.Account;

namespace WebApi.Utilities.Validations.Accounts
{
    public class CreateAccountsValidator : AbstractValidator<NewAccountsDto>
    {
        public CreateAccountsValidator() { 
            RuleFor(e=> e.Guid).NotEmpty();
            RuleFor(e=> e.Password).NotEmpty().MinimumLength(8).MaximumLength(50);
        }
    }
}
