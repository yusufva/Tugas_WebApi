using FluentValidation;
using WebApi.DTOs.Roles;

namespace WebApi.Utilities.Validations.Roles
{
    public class UpdateRolesValidator : AbstractValidator<RoleDto>
    {
        public UpdateRolesValidator()
        {
            RuleFor(e=>e.Guid).NotEmpty(); //rule validator guid
            RuleFor(e => e.Name).NotEmpty().MaximumLength(100); //rule validator name

        }
    }
}
