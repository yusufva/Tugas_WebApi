using FluentValidation;
using WebApi.DTOs.Educations;

namespace WebApi.Utilities.Validations.Educations
{
    public class UpdateEducationsValidator : AbstractValidator<EducationsDto>
    {
        public UpdateEducationsValidator()
        {
            RuleFor(e => e.Guid).NotEmpty(); //rule validator guid
            RuleFor(e => e.UniversityGuid).NotEmpty(); //rule validator university guid
            RuleFor(e => e.Major).NotEmpty().MaximumLength(100); //rule validator major
            RuleFor(e => e.Degree).NotEmpty().MaximumLength(100); //rule validator degree
            RuleFor(e => e.Gpa).NotEmpty(); //rule validator gpa
        }
    }
}
