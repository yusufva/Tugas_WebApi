using FluentValidation;
using WebApi.DTOs.Educations;

namespace WebApi.Utilities.Validations.Educations
{
    public class CreateEducationsValidator : AbstractValidator<NewEducationsDto>
    {
        //public Guid Guid { get; set; } //deklarasi property
        //public Guid UniversityGuid { get; set; } //deklarasi property
        //public string Major { get; set; } //deklarasi property
        //public string Degree { get; set; } //deklarasi property
        //public float Gpa { get; set; } //deklarasi property

        public CreateEducationsValidator()
        {
            RuleFor(e => e.Guid).NotEmpty();
            RuleFor(e => e.UniversityGuid).NotEmpty();
            RuleFor(e => e.Major).NotEmpty().MaximumLength(100);
            RuleFor(e => e.Degree).NotEmpty().MaximumLength(100);
            RuleFor(e => e.Gpa).NotEmpty();
        }
    }
}
