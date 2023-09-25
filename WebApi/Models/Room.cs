using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("tb_m_rooms")]
    public class Room : BaseEntity
    {
        [Column("name", TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [Column("floor", TypeName = "int")]
        public int Floor { get; set; }
        [Column("capacity", TypeName = "int")]
        public int Capacity { get; set; }
    }
}
