using WebApi.Utilities.Enums;
using WebApi.Models;

namespace WebApi.DTOs.Bookings
{
    public class NewBookingsDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLevel Status { get; set; }
        public string Remarks { get; set; }
        public Guid RoomGuid { get; set; }
        public Guid EmployeeGuid { get; set; }

        public static implicit operator Booking(NewBookingsDto dto)
        {
            return new Booking
            {
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = dto.Status,
                Remarks = dto.Remarks,
                RoomGuid = dto.RoomGuid,
                EmployeeGuid = dto.EmployeeGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
