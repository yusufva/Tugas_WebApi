using WebApi.Models;
using WebApi.Utilities.Enums;

namespace WebApi.DTOs.Bookings
{
    public class BookedTodayDto
    {
        public Guid BookingGuid { get; set; }
        public string RoomName { get; set; }
        public StatusLevel Status {  get; set; }
        public int Floor {  get; set; }
        public string BookedBy {  get; set; }

        public static explicit operator BookedTodayDto(Booking booking)
        {
            return new BookedTodayDto
            {
                BookingGuid = booking.Guid,
                Status = booking.Status,
            };
        }   
    }
}
