using WebApi.Models;
using WebApi.Utilities.Enums;

namespace WebApi.DTOs.Bookings
{
    public class BookingsDto : GeneralDto
    {
        public DateTime StartDate { get; set; } //deklarasi property
        public DateTime EndDate { get; set; } //deklarasi property
        public StatusLevel Status { get; set; } //deklarasi property
        public string Remarks { get; set; } //deklarasi property
        public Guid RoomGuid { get; set; } //deklarasi property
        public Guid EmployeeGuid { get; set; } //deklarasi property

        public static explicit operator BookingsDto(Booking booking) //implementasi explicit Operator
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

        public static implicit operator Booking(BookingsDto bookingsDto) //implementasi implicit Operator
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
