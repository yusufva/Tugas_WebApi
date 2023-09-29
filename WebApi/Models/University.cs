using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("tb_m_universities")] //penamaan tabel pada orm
    public class University : BaseEntity
    {
        [Column("code", TypeName = "nvarchar(50)")] //penamaan column pada orm
        public string Code { get; set; } //property pada model
        [Column("name", TypeName = "nvarchar(100)")] //penamaan column pada orm
        public string Name { get; set; } //property pada model

        //cardinality
        public ICollection<Education>? Educations { get; set; }
    }
}
