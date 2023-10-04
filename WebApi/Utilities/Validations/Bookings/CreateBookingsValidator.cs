using FluentValidation;
using WebApi.DTOs.Bookings;

namespace WebApi.Utilities.Validations.Bookings
{
    public class CreateBookingsValidator : AbstractValidator<NewBookingsDto>
    {
        public CreateBookingsValidator() {
            RuleFor(e => e.StartDate).NotEmpty().Must(BeAValidDate); //rule validator start date
            RuleFor(e => e.EndDate).NotEmpty().Must(BeAValidDate); //rule validator end date
            RuleFor(e => e.Status).NotEmpty().IsInEnum(); //rule validator status
            RuleFor(e => e.Remarks).NotEmpty(); //rule validator remarks
            RuleFor(e => e.RoomGuid).NotEmpty(); //rule validator room guid
            RuleFor(e => e.EmployeeGuid).NotEmpty(); //rule validator employee guid
        }

        //custom date validator
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
