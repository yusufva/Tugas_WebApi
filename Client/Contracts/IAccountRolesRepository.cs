using WebApi.DTOs.AccountRoles;
using WebApi.DTOs.Employees;
using WebApi.Models;

namespace Client.Contracts
{
    public interface IAccountRolesRepository : IRepository<AccountRolesDto, NewAccountRolesDto, Guid>
    {
    }
}
