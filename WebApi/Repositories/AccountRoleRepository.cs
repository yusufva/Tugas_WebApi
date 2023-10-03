using WebApi.Contracts;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class AccountRoleRepository : GeneralRepository<AccountRole>, IAccountRoleRepository
    {
        public AccountRoleRepository(BookingManagementDbContext context) : base(context)
        {
        }
    }
}
