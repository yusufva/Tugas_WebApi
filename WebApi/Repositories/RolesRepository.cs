using Microsoft.EntityFrameworkCore;
using WebApi.Contracts;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        private readonly BookingManagementDbContext _context;

        public RolesRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public Role? Create(Role role)
        {
            try
            {
                //ORM melakukan Create
                _context.Set<Role>().Add(role);
                _context.SaveChanges();
                return role;

            }
            catch
            {
                return null;
            }
        }

        public bool Delete(Role role)
        {
            try
            {
                //ORM melakukan Remove
                _context.Set<Role>().Remove(role);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Role> GetAll()
        {
            return _context.Set<Role>().ToList(); // ORM melakukan get all
        }

        public Role? GetByGuid(Guid guid)
        {
            return _context.Set<Role>().Find(guid); // ORM melakukan get by guid
        }

        public bool Update(Role role)
        {
            try
            {
                //ORM melakukan Update
                _context.Set<Role>().Update(role);
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
