using FluentValidation;
using WebApi.DTOs.Rooms;

namespace WebApi.Utilities.Validations.Rooms
{
    public class CreateRoomsValidator : AbstractValidator<NewRoomDto>
    {
        //public string Name { get; set; } //deklarasi property
        //public int Floor { get; set; } //deklarasi property
        //public int Capacity { get; set; } //deklarasi property

        public CreateRoomsValidator() {
            RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
            RuleFor(e=>e.Floor).NotEmpty();
            RuleFor(e=>e.Capacity).NotEmpty();
        }
    }
}
