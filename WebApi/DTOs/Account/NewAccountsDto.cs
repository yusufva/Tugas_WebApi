using WebApi.Models;

namespace WebApi.DTOs.Account
{
    public class NewAccountsDto
    {
        public string Password { get; set; } //deklarasi property

        public static implicit operator Accounts(NewAccountsDto newAccountsDto) //implementasi explicit Operator
        {
            return new Accounts
            {
                Password = newAccountsDto.Password,
                IsUsed = true,
                Otp = 0,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
