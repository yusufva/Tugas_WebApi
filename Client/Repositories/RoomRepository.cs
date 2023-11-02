using Client.Contracts;
using WebApi.DTOs.Employees;
using WebApi.DTOs.Rooms;
using WebApi.Models;

namespace Client.Repositories
{
    public class RoomRepository : GeneralRepository<RoomDto, NewRoomDto, Guid>, IRoomRepository
    {
        public RoomRepository(string request = "Room/") : base(request)
        {
        }
    }
}
