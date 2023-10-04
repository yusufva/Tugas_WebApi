using FluentValidation;
using WebApi.DTOs.Educations;

namespace WebApi.Utilities.Validations.Educations
{
    public class UpdateEducationsValidator : AbstractValidator<EducationsDto>
    {
        public UpdateEducationsValidator()
        {
            RuleFor(e => e.Guid).NotEmpty();
            RuleFor(e => e.UniversityGuid).NotEmpty();
            RuleFor(e => e.Major).NotEmpty().MaximumLength(100);
            RuleFor(e => e.Degree).NotEmpty().MaximumLength(100);
            RuleFor(e => e.Gpa).NotEmpty();
        }
    }
}
