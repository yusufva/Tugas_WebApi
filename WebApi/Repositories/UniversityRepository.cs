using WebApi.Contracts;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class UniversityRepository : GeneralRepository<University>, IUniversityRepository
    {
        public UniversityRepository(BookingManagementDbContext context) : base(context)
        {
        }
    }
}
