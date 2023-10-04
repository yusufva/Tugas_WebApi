using FluentValidation;
using WebApi.DTOs.Bookings;

namespace WebApi.Utilities.Validations.Bookings
{
    public class CreateBookingsValidator : AbstractValidator<NewBookingsDto>
    {
        public CreateBookingsValidator() {
            RuleFor(e => e.StartDate).NotEmpty().Must(BeAValidDate);
            RuleFor(e => e.EndDate).NotEmpty().Must(BeAValidDate);
            RuleFor(e => e.Status).NotEmpty().IsInEnum();
            RuleFor(e => e.Remarks).NotEmpty();
            RuleFor(e => e.RoomGuid).NotEmpty();
            RuleFor(e => e.EmployeeGuid).NotEmpty();
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
