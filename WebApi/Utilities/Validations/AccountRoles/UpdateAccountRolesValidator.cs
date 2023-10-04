using FluentValidation;
using WebApi.DTOs.AccountRoles;

namespace WebApi.Utilities.Validations.AccountRoles
{
    public class UpdateAccountRolesValidator : AbstractValidator<AccountRolesDto>
    {
        public UpdateAccountRolesValidator() {
            RuleFor(e => e.Guid).NotEmpty(); //rule validator guid
            RuleFor(e => e.AccountGuid).NotEmpty(); //rule validator account guid
            RuleFor(e => e.RoleGuid).NotEmpty(); //rule validator roleguid
        }
    }
}
