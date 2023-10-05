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

        public University? GetByCode(string code)
        {
            var entity = _context.Set<University>().Where(u => u.Code == code).FirstOrDefault();
            _context.ChangeTracker.Clear();
            return entity;
        }
    }
}
