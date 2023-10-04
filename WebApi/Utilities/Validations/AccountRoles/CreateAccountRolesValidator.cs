using FluentValidation;
using WebApi.DTOs.AccountRoles;

namespace WebApi.Utilities.Validations.AccountRoles
{
    public class CreateAccountRolesValidator : AbstractValidator<NewAccountRolesDto>
    {
        public CreateAccountRolesValidator() {
            RuleFor(e => e.AccountGuid).NotEmpty();
            RuleFor(e => e.RoleGuid).NotEmpty();
        }
    }
}
