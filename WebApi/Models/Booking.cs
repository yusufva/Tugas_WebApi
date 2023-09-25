using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Utilities.Enums;

namespace WebApi.Models
{
    [Table("tb_tr_bookings")]
    public class Booking : BaseEntity
    {
        [Column("start_date", TypeName = "datetime2")]
        public DateTime StartDate { get; set; }
        [Column("end_date", TypeName = "datetime2")]
        public DateTime EndDate { get; set; }
        [Column("status", TypeName = "int")]
        public StatusLevel Status { get; set; }
        [Column("remarks", TypeName = "nvarchar(max)")]
        public string Remarks { get; set; }
        [Column("room_guid")]
        public Guid RoomGuid { get; set; }
        [Column("room_guid")]
        public Guid EmployeeGuid { get; set; }
    }
}
