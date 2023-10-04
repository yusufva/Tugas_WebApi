using FluentValidation;
using WebApi.DTOs.Universities;

namespace WebApi.Utilities.Validations.Universities
{
    public class UpdateUniversityValidator : AbstractValidator<UniversityDto>
    {
        public UpdateUniversityValidator()
        {
            RuleFor(e => e.Guid).NotEmpty(); //rule validator guid
            RuleFor(e => e.Name).NotEmpty(); //rule validator name
            RuleFor(e => e.Code).NotEmpty(); //rule validator code
        }
    }
}
