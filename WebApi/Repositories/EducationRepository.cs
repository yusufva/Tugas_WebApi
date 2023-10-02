using WebApi.Contracts;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class EducationRepository : IEducationRepository
    {
        private readonly BookingManagementDbContext _context;

        public EducationRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public Education? Create(Education education)
        {
            try
            {
                //ORM melakukan Create
                _context.Set<Education>().Add(education);
                _context.SaveChanges();
                return education;

            }
            catch
            {
                return null;
            }
        }

        public bool Delete(Education education)
        {
            try
            {
                //ORM melakukan Remove
                _context.Set<Education>().Remove(education);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Education> GetAll()
        {
            return _context.Set<Education>().ToList(); // ORM melakukan get all
        }

        public Education? GetByGuid(Guid guid)
        {
            return _context.Set<Education>().Find(guid); // ORM melakukan get by guid
        }

        public bool Update(Education education)
        {
            try
            {
                //ORM melakukan Update
                _context.Set<Education>().Update(education);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
