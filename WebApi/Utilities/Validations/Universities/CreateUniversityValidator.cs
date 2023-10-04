using FluentValidation;
using WebApi.DTOs.Universities;

namespace WebApi.Utilities.Validations.Universities
{
    public class CreateUniversityValidator : AbstractValidator<CreateUniversityDto>
    {
        //public string Name { get; set; } //deklarasi property
        //public string Code { get; set; } //deklarasi property

        public CreateUniversityValidator()
        {
            RuleFor(e => e.Name).NotEmpty().MaximumLength(100); //rule validator name
            RuleFor(e => e.Code).NotEmpty().MaximumLength(50); //rule validator code
        }
    }
}
