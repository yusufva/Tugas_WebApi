using FluentValidation;
using WebApi.DTOs.Account;

namespace WebApi.Utilities.Validations.Employees
{
    //public string FirstName { get; set; }
    //public string LastName { get; set; }
    //public DateTime BirthDate { get; set; }
    //public GenderLevel Gender { get; set; }
    //public DateTime HiringDate { get; set; }
    //public string Email { get; set; }
    //public string PhoneNumber { get; set; }
    //public string Major { get; set; }
    //public string Degree { get; set; }
    //public float GPA { get; set; }
    //public string UniversityCode { get; set; }
    //public string UniversityName { get; set; }
    //public string Password { get; set; }
    //public string ConfirmPassword { get; set; }

    public class RegisterEmployeeValidator : AbstractValidator<AccountRegisterRequestDto>
    {
        public RegisterEmployeeValidator()
        {
            RuleFor(e=>e.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(e=>e.LastName).NotEmpty().MaximumLength(100);
            RuleFor(e=>e.BirthDate).NotEmpty().LessThanOrEqualTo(DateTime.Now.AddYears(-18));
            RuleFor(e=>e.Gender).NotNull().IsInEnum();
            RuleFor(e=>e.HiringDate).NotEmpty();
            RuleFor(e=>e.Email).NotEmpty().EmailAddress();
            RuleFor(e=>e.PhoneNumber).NotEmpty().Length(10, 20);
            RuleFor(e => e.Major).NotEmpty().MaximumLength(100); //rule validator major
            RuleFor(e => e.Degree).NotEmpty().MaximumLength(100); //rule validator degree
            RuleFor(e => e.GPA).NotEmpty(); //rule validator gpa
            RuleFor(e=>e.UniversityCode).NotEmpty().MaximumLength(50);
            RuleFor(e=>e.UniversityName).NotEmpty().MaximumLength(100);
            RuleFor(e=>e.Password).NotEmpty().Length(8,50);
            RuleFor(e=>e.ConfirmPassword).NotEmpty().Equal(e=>e.Password);
        }
    }
}
