using WebApi.Models;
using WebApi.Utilities.Enums;

namespace WebApi.DTOs.Bookings
{
    public class BookingsDto
    {
        public Guid Guid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLevel Status { get; set; }
        public string Remarks { get; set; }
        public Guid RoomGuid { get; set; }
        public Guid EmployeeGuid { get; set; }

        public static explicit operator BookingsDto(Booking booking)
        {
            return new BookingsDto
            {
                Guid = booking.Guid,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Status = booking.Status,
                Remarks = booking.Remarks,
                RoomGuid = booking.RoomGuid,
                EmployeeGuid = booking.EmployeeGuid,
            };
        }

        public static implicit operator Booking(BookingsDto bookingsDto)
        {
            return new Booking
            {
                Guid = bookingsDto.Guid,
                StartDate = bookingsDto.StartDate,
                EndDate = bookingsDto.EndDate,
                Status = bookingsDto.Status,
                Remarks = bookingsDto.Remarks,
                RoomGuid = bookingsDto.RoomGuid,
                EmployeeGuid = bookingsDto.EmployeeGuid,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
