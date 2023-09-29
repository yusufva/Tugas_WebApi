using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("tb_m_account_roles")] //penamaan tabel pada orm
    public class AccountRole : BaseEntity
    {
        [Column("account_guid")] //penamaan column pada orm
        public Guid AccountGuid { get; set; } //property pada model
        [Column("role_guid")] //penamaan column pada orm
        public Guid RoleGuid { get; set; } //property pada model

        //cardinality
        public Role? Role { get; set; }
        public Accounts? Accounts { get; set; }
    }
}
