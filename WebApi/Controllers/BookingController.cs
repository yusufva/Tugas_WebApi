using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.DTOs.Bookings;
using WebApi.Models;
using WebApi.Utilities.Handler;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public BookingController(IBookingRepository bookingRepository, IRoomRepository roomRepository, IEmployeeRepository employeeRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _employeeRepository = employeeRepository;
        }

        //Logic untuk Get Booking
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _bookingRepository.GetAll(); //mengambil semua data Booking
            if (!result.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data not found"));
            }

            var data = result.Select(x => (BookingsDto)x);

            return Ok(new ResponseOkHandler<IEnumerable<BookingsDto>>(data, "Data retrieve Successfully"));
        }

        // mengambil data detail booking
        [HttpGet("detail")]
        public IActionResult GetDetail()
        {
            var booking = _bookingRepository.GetAll(); //mengambil data booking
            var room = _roomRepository.GetAll(); //mengambil data room
            var employee = _employeeRepository.GetAll(); //mengambil data employee
            if (!employee.Any() || !booking.Any() || !room.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data not found")); //response jika data tidak ditemukan
            }

            //LinQ untuk melakukan join dan mengambil data yang dibutuhkan
            var getAll = from b in booking
                         join r in room on b.RoomGuid equals r.Guid
                         join e in employee on b.EmployeeGuid equals e.Guid
                         where b.StartDate == DateTime.Today || b.EndDate == DateTime.Today
                         select new BookingDetailDto
                         {
                             Guid = b.Guid,
                             BookedNik = e.Nik,
                             BookedBy = string.Concat(e.FirstName, " ", e.LastName),
                             RoomName = r.Name,
                             StartDate = b.StartDate,
                             EndDate = b.EndDate,
                             Status = b.Status,
                             Remarks = b.Remarks,
                         };

            if (!getAll.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data not found")); //response jika data tidak ditemukan
            }

            return Ok(new ResponseOkHandler<IEnumerable<BookingDetailDto>>(getAll, "Data retrieve Successfully")); //response untuk menampilkan data
        }
        
        // mengambil data booking detail by guid
        [HttpGet("detail/{guid}")]
        public IActionResult GetDetailByGuid(Guid guid)
        {
            var booking = _bookingRepository.GetAll(); //mengambil data booking
            var room = _roomRepository.GetAll(); //mengambil data room
            var employee = _employeeRepository.GetAll(); //mengambil data employee
            if (!employee.Any() || !booking.Any() || !room.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data not found")); //response jika data tidak ditemukan
            }

            //LinQ untuk melakukan join dan mengambil data yang dibutuhkan
            var getAll = from b in booking
                         join r in room on b.RoomGuid equals r.Guid
                         join e in employee on b.EmployeeGuid equals e.Guid
                         where b.Guid == guid
                         select new BookingDetailDto
                         {
                             Guid = b.Guid,
                             BookedNik = e.Nik,
                             BookedBy = string.Concat(e.FirstName, " ", e.LastName),
                             RoomName = r.Name,
                             StartDate = b.StartDate,
                             EndDate = b.EndDate,
                             Status = b.Status,
                             Remarks = b.Remarks,
                         };
            if (!getAll.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data not found")); //response jika data tidak ditemukan
            }

            return Ok(new ResponseOkHandler<IEnumerable<BookingDetailDto>>(getAll, "Data retrieve Successfully")); //response untuk menampilkan data
        }

        //Logic untuk Get Booking/{guid}
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _bookingRepository.GetByGuid(guid); //mengambil data Booking By Guid
            if (result is null)
            {
                return NotFound(new ResponseNotFoundHandler("Id not found"));
            }

            return Ok(new ResponseOkHandler<BookingsDto>((BookingsDto)result, "Data retrieve Successfully"));
        }

        //mengambil data lama booking ruangan
        [HttpGet("length")]
        public IActionResult GetBookingLength()
        {
            var allBookings = _bookingRepository.GetAll(); //mengambil data booking
            var allRooms = _roomRepository.GetAll(); //mengambil data room

            //LinQ untuk melakukan join dan mengambil data booking length
            var bookingLength = from ab in allBookings
                                join ar in allRooms on ab.RoomGuid equals ar.Guid
                                select new
                                {
                                    ab.RoomGuid,
                                    RoomName =  ar.Name,
                                    BookingLength = string.Concat(WorkingDaysHandler.CalculateWorkingDays(ab.StartDate, ab.EndDate), " days")
                                };
            if (!bookingLength.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data not found")); //response jika data tidak ditemukan
            }

            return Ok(new ResponseOkHandler<IEnumerable<object>>(bookingLength, "data retrieve successfully")); //response untuk menampilkan data
        }

        //Logic untuk Post Booking/
        [HttpPost]
        public IActionResult Insert(NewBookingsDto newBookingsDto)
        {
            try
            {
                var result = _bookingRepository.Create(newBookingsDto); //melakukan Create Booking

                return Ok(new ResponseOkHandler<BookingsDto>((BookingsDto)result, "Insert Success"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("failed to Create Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk PUT Booking
        [HttpPut]
        public IActionResult Update(BookingsDto bookingsDto)
        {

            try
            {
                var entity = _bookingRepository.GetByGuid(bookingsDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Id not Found"));
                }

                _bookingRepository.Update(bookingsDto); //melakukan update Booking

                return Ok(new ResponseOkHandler<BookingsDto>("Data has been Updated"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Update Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk Delete Booking
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var booking = _bookingRepository.GetByGuid(guid); //mengambil booking by GUID
                if (booking is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Id not Found"));
                }

                _bookingRepository.Delete(booking); //melakukan Delete Booking

                return Ok(new ResponseOkHandler<BookingsDto>("Data has been Deleted"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Delete Data", ex.Message)); //error pada repository
            }
        }
    }
}
