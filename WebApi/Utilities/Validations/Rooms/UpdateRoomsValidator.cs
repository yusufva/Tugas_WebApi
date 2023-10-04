using FluentValidation;
using WebApi.DTOs.Rooms;

namespace WebApi.Utilities.Validations.Rooms
{
    public class UpdateRoomsValidator : AbstractValidator<RoomDto>
    {
        public UpdateRoomsValidator()
        {
            RuleFor(e => e.Guid).NotEmpty(); //rule validator guid
            RuleFor(e => e.Name).NotEmpty().MaximumLength(100); //rule validator name
            RuleFor(e => e.Floor).NotEmpty(); //rule validator floor
            RuleFor(e => e.Capacity).NotEmpty(); //rule validator capacity
        }
    }
}
