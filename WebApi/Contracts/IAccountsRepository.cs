using WebApi.DTOs.Account;
using WebApi.Models;

namespace WebApi.Contracts
{
    public interface IAccountsRepository : IGeneralRepository<Accounts>
    {
        AccountRegisterRequestDto? Register(AccountRegisterRequestDto request, bool isValid);
    }
}
