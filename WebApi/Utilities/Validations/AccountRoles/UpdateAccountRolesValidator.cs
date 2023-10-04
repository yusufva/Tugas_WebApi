using FluentValidation;
using WebApi.DTOs.AccountRoles;

namespace WebApi.Utilities.Validations.AccountRoles
{
    public class UpdateAccountRolesValidator : AbstractValidator<AccountRolesDto>
    {
        public UpdateAccountRolesValidator() {
            RuleFor(e => e.Guid).NotEmpty();
            RuleFor(e => e.AccountGuid).NotEmpty();
            RuleFor(e => e.RoleGuid).NotEmpty();
        }
    }
}
