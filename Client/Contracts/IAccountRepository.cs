using Client.Models;
using WebApi.DTOs.Account;
using WebApi.DTOs.Employees;
using WebApi.Models;
using WebApi.Utilities.Handler;

namespace Client.Contracts
{
    public interface IAccountRepository : IRepository<AccountsDto, NewAccountsDto, Guid>
    {
        Task<ResponseOkHandler<TokenDto>> Login(EmployeeLoginDto login);
    }
}
