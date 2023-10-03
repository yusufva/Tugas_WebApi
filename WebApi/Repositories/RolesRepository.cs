using WebApi.Contracts;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class RolesRepository : GeneralRepository<Role>, IRolesRepository
    {
        public RolesRepository(BookingManagementDbContext context) : base(context)
        {
        }
    }
}
