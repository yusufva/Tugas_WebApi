using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.DTOs.Bookings;
using WebApi.DTOs.Rooms;
using WebApi.Repositories;
using WebApi.Utilities.Handler;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public RoomController(IRoomRepository roomRepository, IBookingRepository bookingRepository, IEmployeeRepository employeeRepository)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
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

        // mengambil data ruangan yang tersedia
        [HttpGet("available")]
        public IActionResult Available()
        {
            var today = DateTime.Today; //mendefinisikan variabel hari ini

            var bookings = _bookingRepository.GetAll(); //mengambil data booking
            var rooms = _roomRepository.GetAll(); //mengambil data room

            //LinQ untuk melakukan join dan mengambil data booking hari ini
            var bookingToday = from b in bookings
                               join r in rooms on b.RoomGuid equals r.Guid
                               where b.StartDate == DateTime.Today || b.EndDate == DateTime.Today
                               select b.RoomGuid;

            //LinQ untuk melakukan join dan mengambil data room yang tidak di booking
            var availableRoom = from r in rooms
                                where !bookingToday.Contains(r.Guid)
                                select new
                                {
                                    RoomGuid = r.Guid,
                                    RoomName = r.Name,
                                    r.Floor,
                                    r.Capacity
                                };
            if (!availableRoom.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data not found")); //response jika data tidak ditemukan
            }

            return Ok(new ResponseOkHandler<IEnumerable<object>>(availableRoom, "data retrieve successfully")); //response untuk menampilkan data
        }

        //mengambil data ruangan yang sedang dipakai hari ini
        [HttpGet("inused")]
        public IActionResult GetInUsed()
        {
            var booking = _bookingRepository.GetAll(); //mengambil data booking
            var room = _roomRepository.GetAll(); //mengambil data room
            var employee = _employeeRepository.GetAll(); //mengambil data employee

            //LinQ untuk melakukan join dan mengambil data yang dibutuhkan
            var inUsed = from b in booking
                         join r in room on b.RoomGuid equals r.Guid
                         join e in employee on b.EmployeeGuid equals e.Guid
                         where b.StartDate == DateTime.Today || b.EndDate == DateTime.Today
                         select new BookedTodayDto
                         {
                             BookingGuid = b.Guid,
                             RoomName = r.Name,
                             Status = b.Status,
                             Floor = r.Floor,
                             BookedBy = string.Concat(e.FirstName, " ", e.LastName)
                         };
            if (!inUsed.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data not found")); //response jika data tidak ditemukan
            }


            return Ok(new ResponseOkHandler<IEnumerable<BookedTodayDto>>(inUsed, "Data retrieve successfully")); //response untuk menampilkan data
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
