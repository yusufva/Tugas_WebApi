using WebApi.Models;

namespace WebApi.DTOs.Account
{
    public class NewAccountsDto : GeneralDto
    {
        public string Password { get; set; } //deklarasi property

        public static implicit operator Accounts(NewAccountsDto newAccountsDto) //implementasi explicit Operator
        {
            return new Accounts
            {
                Guid = newAccountsDto.Guid,
                Password = newAccountsDto.Password,
                IsUsed = true,
                Otp = 0,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
