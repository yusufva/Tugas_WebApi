using WebApi.Models;

namespace WebApi.DTOs.Rooms
{
    public class NewRoomDto
    {
        public string Name { get; set; } //deklarasi property
        public int Floor { get; set; } //deklarasi property
        public int Capacity { get; set; } //deklarasi property

        public static implicit operator Room(NewRoomDto newRoomDto) //implementasi implicit Operator
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
