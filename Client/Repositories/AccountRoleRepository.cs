using Client.Contracts;
using WebApi.DTOs.AccountRoles;
using WebApi.DTOs.Employees;
using WebApi.Models;

namespace Client.Repositories
{
    public class AccountRoleRepository : GeneralRepository<AccountRolesDto, NewAccountRolesDto, Guid>, IAccountRolesRepository
    {
        public AccountRoleRepository(string request = "AccountRole/") : base(request)
        {
        }
    }
}
