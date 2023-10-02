using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Utilities.Enums;

namespace WebApi.Models
{
    [Table("tb_tr_bookings")] //penamaan tabel pada orm
    public class Booking : BaseEntity
    {
        [Column("start_date", TypeName = "datetime2")] //penamaan column pada orm
        public DateTime StartDate { get; set; } //property pada model
        [Column("end_date", TypeName = "datetime2")] //penamaan column pada orm
        public DateTime EndDate { get; set; } //property pada model
        [Column("status", TypeName = "int")] //penamaan column pada orm
        public StatusLevel Status { get; set; } //property pada model
        [Column("remarks", TypeName = "nvarchar(max)")] //penamaan column pada orm
        public string Remarks { get; set; } //property pada model
        [Column("room_guid")] //penamaan column pada orm
        public Guid RoomGuid { get; set; } //property pada model
        [Column("employee_guid")] //penamaan column pada orm
        public Guid EmployeeGuid { get; set; } //property pada model

        //cardinality
        public Employee? Employee { get; set; }
        public Room? Room { get; set; }
    }
}
