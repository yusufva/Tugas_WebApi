using FluentValidation;
using WebApi.DTOs.Rooms;

namespace WebApi.Utilities.Validations.Rooms
{
    public class UpdateRoomsValidator : AbstractValidator<RoomDto>
    {
        public UpdateRoomsValidator()
        {
            RuleFor(e => e.Guid).NotEmpty();
            RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
            RuleFor(e => e.Floor).NotEmpty();
            RuleFor(e => e.Capacity).NotEmpty();
        }
    }
}
