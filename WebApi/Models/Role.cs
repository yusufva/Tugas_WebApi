using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("tb_m_roles")]
    public class Role : BaseEntity
    {
        [Column("name", TypeName = "nvarchar(100)")]
        public string Name { get; set; }
    }
}
