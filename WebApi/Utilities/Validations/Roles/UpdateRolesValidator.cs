using FluentValidation;
using WebApi.DTOs.Roles;

namespace WebApi.Utilities.Validations.Roles
{
    public class UpdateRolesValidator : AbstractValidator<RoleDto>
    {
        public UpdateRolesValidator()
        {
            RuleFor(e=>e.Guid).NotEmpty();
            RuleFor(e => e.Name).NotEmpty().MaximumLength(100);

        }
    }
}
