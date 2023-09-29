using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("tb_m_accounts")] //penamaan tabel pada orm
    public class Accounts : BaseEntity
    {
        [Column("password", TypeName = "nvarchar(max)")] //penamaan column pada orm
        public string Password { get; set; } //property pada model
        [Column("otp", TypeName = "int")] //penamaan column pada orm
        public int Otp {  get; set; } //property pada model
        [Column("is_used")] //penamaan column pada orm
        public bool IsUsed { get; set; } //property pada model
        [Column("exoired_time", TypeName = "datetime2")] //penamaan column pada orm
        public DateTime ExpiredTime { get; set; } //property pada model

        //cardinality
        public ICollection<AccountRole>? AccountRoles { get; set; }
        public Employee? Employee { get; set; }
    }
}
