using FluentValidation;
using WebApi.DTOs.Employees;

namespace WebApi.Utilities.Validations.Employees
{
    public class CreateEmployeeValidator : AbstractValidator<NewEmployeesDto>
    {
        public CreateEmployeeValidator() {
            RuleFor(e=>e.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(e => e.LastName).MaximumLength(100);
            RuleFor(e => e.BirthDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now.AddYears(-18)); //18 y.o
            RuleFor(e => e.Gender)
                .NotEmpty()
                .IsInEnum();
            RuleFor(e => e.HiringDate)
                .NotEmpty();
            RuleFor(e => e.Email)
                .NotEmpty()
                .EmailAddress().MaximumLength(100);
            RuleFor(e => e.PhoneNumber)
                .NotEmpty()
                .Length(10,20);
        }
    }
}
