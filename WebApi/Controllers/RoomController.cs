using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.DTOs.Roles;
using WebApi.DTOs.Rooms;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        //Logic untuk Get Room
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _roomRepository.GetAll(); //mengambil semua data Room
            if (!result.Any())
            {
                return NotFound("Data not found");
            }

            var data = result.Select(x => (RoomDto)x);

            return Ok(data);
        }

        //Logic untuk Get Room/{guid}
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _roomRepository.GetByGuid(guid); //mengambil data Room By Guid
            if (result is null)
            {
                return NotFound("Id not found");
            }

            return Ok((RoomDto)result);
        }

        //Logic untuk Post Room/
        [HttpPost]
        public IActionResult Insert(NewRoomDto newRoomDto)
        {
            var result = _roomRepository.Create(newRoomDto); //melakukan Create Room
            if (result is null)
            {
                return BadRequest("Failed to Create Data");
            }

            return Ok((RoomDto)result);
        }

        //Logic untuk PUT Room
        [HttpPut]
        public IActionResult Update(RoomDto roomDto)
        {
            var entity = _roomRepository.GetByGuid(roomDto.Guid);
            if (entity is null)
            {
                return NotFound("Id not Found");
            }

            var result = _roomRepository.Update(roomDto); //melakukan update Room
            if (!result)
            {
                return BadRequest("Failed to Update Data");
            }

            return Ok("Data has been Updated");
        }

        //Logic untuk Delete Room
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var room = _roomRepository.GetByGuid(guid); //mengambil room by GUID
            if (room is null)
            {
                return NotFound("Id not Found");
            }

            var result = _roomRepository.Delete(room); //melakukan Delete Room
            if (!result)
            {
                return BadRequest("Id not found");
            }

            return Ok("Room has been deleted");
        }
    }
}
