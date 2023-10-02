using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Models;

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
                return NotFound("Data not found");
            }

            return Ok(result);
        }

        //Logic untuk Get Booking/{guid}
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _bookingRepository.GetByGuid(guid); //mengambil data Booking By Guid
            if (result is null)
            {
                return NotFound("Id not found");
            }

            return Ok(result);
        }

        //Logic untuk Post Booking/
        [HttpPost]
        public IActionResult Insert(Booking booking)
        {
            var result = _bookingRepository.Create(booking); //melakukan Create Booking
            if (result is null)
            {
                return BadRequest("Failed to Create Data");
            }

            return Ok(result);
        }

        //Logic untuk PUT Booking
        [HttpPut]
        public IActionResult Update(Booking booking)
        {
            var result = _bookingRepository.Update(booking); //melakukan update Booking
            if (!result)
            {
                return BadRequest("Failed to Update Data");
            }

            return Ok("Data has been Updated");
        }

        //Logic untuk Delete Booking
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var booking = _bookingRepository.GetByGuid(guid); //mengambil booking by GUID
            var result = _bookingRepository.Delete(booking); //melakukan Delete Booking
            if (!result)
            {
                return BadRequest("Id not found");
            }

            return Ok("Booking has been deleted");
        }
    }
}
