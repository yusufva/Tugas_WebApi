using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("tb_m_educations")]
    public class Education : BaseEntity 
    {
        [Column("major", TypeName = "nvarchar(100)")]
        public string Major { get; set; }
        [Column("degree", TypeName = "nvarchar(100)")]
        public string Degree { get; set; }
        [Column("gpa", TypeName = "decimal(6,2)")]
        public float Gpa { get; set; }
        [Column("university_guid", TypeName = "guid")]
        public Guid UniversityGuid { get; set; }
    }
}
