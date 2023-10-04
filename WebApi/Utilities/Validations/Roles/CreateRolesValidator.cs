using FluentValidation;
using WebApi.DTOs.Roles;

namespace WebApi.Utilities.Validations.Roles
{
    public class CreateRolesValidator : AbstractValidator<NewRoleDto>
    {
        //public string Name { get; set; } //deklarasi property

        public CreateRolesValidator() {
            RuleFor(e => e.Name).NotEmpty().MaximumLength(100); //rule validator name
        }
    }
}
