using FluentValidation;
using WebApi.DTOs.Account;

namespace WebApi.Utilities.Validations.Accounts
{
    public class CreateAccountsValidator : AbstractValidator<NewAccountsDto>
    {
        public CreateAccountsValidator() { 
            RuleFor(e=> e.Guid).NotEmpty(); //rule validator guid
            RuleFor(e=> e.Password).NotEmpty().Length(8,50); //rule validator password
        }
    }
}
