using WebApi.Models;

namespace WebApi.DTOs.Rooms
{
    public class NewRoomDto
    {
        public string Name { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }

        public static implicit operator Room(NewRoomDto newRoomDto)
        {
            return new Room
            {
                Name = newRoomDto.Name,
                Floor = newRoomDto.Floor,
                Capacity = newRoomDto.Capacity,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
