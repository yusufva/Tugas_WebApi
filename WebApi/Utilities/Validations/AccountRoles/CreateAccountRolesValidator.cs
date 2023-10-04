using FluentValidation;
using WebApi.DTOs.AccountRoles;

namespace WebApi.Utilities.Validations.AccountRoles
{
    public class CreateAccountRolesValidator : AbstractValidator<NewAccountRolesDto>
    {
        public CreateAccountRolesValidator() {
            RuleFor(e => e.AccountGuid).NotEmpty(); //rule validator account guid
            RuleFor(e => e.RoleGuid).NotEmpty(); //rule validator role guid
        }
    }
}
