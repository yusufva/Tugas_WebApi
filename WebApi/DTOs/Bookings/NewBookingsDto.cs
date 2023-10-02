using WebApi.Utilities.Enums;
using WebApi.Models;

namespace WebApi.DTOs.Bookings
{
    public class NewBookingsDto
    {
        public DateTime StartDate { get; set; } //deklarasi property
        public DateTime EndDate { get; set; } //deklarasi property
        public StatusLevel Status { get; set; } //deklarasi property
        public string Remarks { get; set; } //deklarasi property
        public Guid RoomGuid { get; set; } //deklarasi property
        public Guid EmployeeGuid { get; set; } //deklarasi property

        public static implicit operator Booking(NewBookingsDto dto) //implementasi implicit Operator
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
