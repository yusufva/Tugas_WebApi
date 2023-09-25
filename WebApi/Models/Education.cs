using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("tb_m_educations")] //penamaan tabel pada orm
    public class Education : BaseEntity 
    {
        [Column("major", TypeName = "nvarchar(100)")] //penamaan column pada orm
        public string Major { get; set; } //property pada model
        [Column("degree", TypeName = "nvarchar(100)")] //penamaan column pada orm
        public string Degree { get; set; } //property pada model
        [Column("gpa", TypeName = "decimal(6,2)")] //penamaan column pada orm
        public float Gpa { get; set; } //property pada model
        [Column("university_guid", TypeName = "guid")] //penamaan column pada orm
        public Guid UniversityGuid { get; set; } //property pada model
    }
}
