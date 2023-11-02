using WebApi.DTOs.Employees;
using WebApi.DTOs.Rooms;
using WebApi.Models;

namespace Client.Contracts
{
    public interface IRoomRepository : IRepository<RoomDto, NewRoomDto, Guid>
    {
    }
}
