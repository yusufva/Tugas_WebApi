using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("tb_m_roles")] //penamaan tabel pada orm
    public class Role : BaseEntity
    {
        [Column("name", TypeName = "nvarchar(100)")] //penamaan column pada orm
        public string Name { get; set; } //property pada model

        //cardinality
        public ICollection<AccountRole>? AccountRoles { get; set; }
    }
}
