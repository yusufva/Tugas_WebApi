using WebApi.Contracts;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly BookingManagementDbContext _context;

        public UniversityRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public University? Create(University university)
        {
            try
            {
                //ORM melakukan Create
                _context.Set<University>().Add(university);
                _context.SaveChanges();
                return university;

            } catch
            {
                return null;
            }
        }

        public bool Delete(University university)
        {
            try
            {
                //ORM melakukan Remove
                _context.Set<University>().Remove(university);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<University> GetAll()
        {
            return _context.Set<University>().ToList(); // ORM melakukan get all
        }

        public University? GetByGuid(Guid guid)
        {
            return _context.Set<University>().Find(guid); // ORM melakukan get by guid
        }

        public bool Update(University university)
        {
            try
            {
                //ORM melakukan Update
                _context.Set<University>().Update(university);
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
