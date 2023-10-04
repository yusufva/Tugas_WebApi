using FluentValidation;
using WebApi.DTOs.Universities;

namespace WebApi.Utilities.Validations.Universities
{
    public class UpdateUniversityValidator : AbstractValidator<UniversityDto>
    {
        public UpdateUniversityValidator()
        {
            RuleFor(e => e.Guid).NotEmpty();
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.Code).NotEmpty();
        }
    }
}
