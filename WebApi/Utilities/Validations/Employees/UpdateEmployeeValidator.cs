using FluentValidation;
using WebApi.DTOs.Employees;

namespace WebApi.Utilities.Validations.Employees
{
    public class UpdateEmployeeValidator : AbstractValidator<EmployeesDto>
    {
        public UpdateEmployeeValidator() {
            RuleFor(e => e.Guid).NotEmpty();
            RuleFor(e => e.FirstName).NotEmpty();
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
                .EmailAddress();
            RuleFor(e => e.PhoneNumber)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(20);
        }
    }
}
