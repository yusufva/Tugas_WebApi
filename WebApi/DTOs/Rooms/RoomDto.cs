using WebApi.Models;

namespace WebApi.DTOs.Rooms
{
    public class RoomDto : GeneralDto
    {
        public string Name { get; set; } //deklarasi property
        public int Floor { get; set; } //deklarasi property
        public int Capacity { get; set; } //deklarasi property

        public static explicit operator RoomDto(Room room) //implementasi explicit Operator
        {
            return new RoomDto
            {
                Guid = room.Guid,
                Name = room.Name,
                Floor = room.Floor,
                Capacity = room.Capacity,
            };
        }

        public static implicit operator Room(RoomDto roomDto) //implementasi implicit Operator
        {
            return new Room
            {
                Guid = roomDto.Guid,
                Name = roomDto.Name,
                Floor = roomDto.Floor,
                Capacity = roomDto.Capacity,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
