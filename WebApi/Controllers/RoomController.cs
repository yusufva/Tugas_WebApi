using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.DTOs.Employees;
using WebApi.DTOs.Roles;
using WebApi.DTOs.Rooms;
using WebApi.Repositories;
using WebApi.Utilities.Handler;

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
                return NotFound(new ResponseNotFoundHandler("Data not found"));
            }

            var data = result.Select(x => (RoomDto)x);

            return Ok(new ResponseOkHandler<IEnumerable<RoomDto>>(data, "Data retrieve Successfully"));
        }

        //Logic untuk Get Room/{guid}
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _roomRepository.GetByGuid(guid); //mengambil data Room By Guid
            if (result is null)
            {
                return NotFound(new ResponseNotFoundHandler("Id not found"));
            }

            return Ok(new ResponseOkHandler<RoomDto>((RoomDto)result, "Data retrieve Successfully"));
        }

        //Logic untuk Post Room/
        [HttpPost]
        public IActionResult Insert(NewRoomDto newRoomDto)
        {
            try
            {
                var result = _roomRepository.Create(newRoomDto); //melakukan Create Room

                return Ok(new ResponseOkHandler<RoomDto>((RoomDto)result, "Insert Success"));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("failed to Create Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk PUT Room
        [HttpPut]
        public IActionResult Update(RoomDto roomDto)
        {
            try
            {
                var entity = _roomRepository.GetByGuid(roomDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Id not Found"));
                }

                _roomRepository.Update(roomDto); //melakukan update Room

                return Ok(new ResponseOkHandler<RoomDto>("Data has been Updated"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Update Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk Delete Room
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var room = _roomRepository.GetByGuid(guid); //mengambil room by GUID
                if (room is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Id not Found"));
                }

                _roomRepository.Delete(room); //melakukan Delete Room

                return Ok(new ResponseOkHandler<RoomDto>("Data has been Deleted"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Delete Data", ex.Message)); //error pada repository
            }
        }
    }
}
