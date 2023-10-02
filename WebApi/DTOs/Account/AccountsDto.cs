using WebApi.Models;

namespace WebApi.DTOs.Account
{
    public class AccountsDto : GeneralDto
    {
        public int Otp {  get; set; }
        public bool IsUsed { get; set; }

        public static explicit operator AccountsDto(Accounts accounts)
        {
            return new AccountsDto
            {
                Guid = accounts.Guid,
                Otp = accounts.Otp,
                IsUsed = accounts.IsUsed
            };
        }

        public static implicit operator Accounts(AccountsDto accountsDto)
        {
            return new Accounts
            {
                Guid = accountsDto.Guid,
                Otp = accountsDto.Otp,
                IsUsed = accountsDto.IsUsed,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
