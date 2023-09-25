using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("tb_m_accounts")]
    public class Accounts : BaseEntity
    {
        [Column("password", TypeName = "nvarchar(max)")]
        public string Password { get; set; }
        [Column("otp", TypeName = "int")]
        public int Otp {  get; set; }
        [Column("is_used")]
        public bool IsUsed { get; set; }
        [Column("exoired_time", TypeName = "datetime2")]
        public DateTime ExpiredTime { get; set; }

    }
}
