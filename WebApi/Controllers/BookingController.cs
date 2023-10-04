using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.DTOs.Bookings;
using WebApi.DTOs.Employees;
using WebApi.Utilities.Handler;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
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

                return Ok(new ResponseOkHandler<EmployeesDto>("Data has been Deleted"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Delete Data", ex.Message)); //error pada repository
            }
        }
    }
}
