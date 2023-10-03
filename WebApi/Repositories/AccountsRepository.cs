using WebApi.Contracts;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class AccountsRepository : GeneralRepository<Accounts>, IAccountsRepository
    {
        public AccountsRepository(BookingManagementDbContext context) : base(context){  }
    }
}
