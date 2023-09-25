using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("tb_m_rooms")] //penamaan tabel pada orm
    public class Room : BaseEntity
    {
        [Column("name", TypeName = "nvarchar(100)")] //penamaan column pada orm
        public string Name { get; set; } //property pada model
        [Column("floor", TypeName = "int")] //penamaan column pada orm
        public int Floor { get; set; } //property pada model
        [Column("capacity", TypeName = "int")] //penamaan column pada orm
        public int Capacity { get; set; } //property pada model
    }
}
