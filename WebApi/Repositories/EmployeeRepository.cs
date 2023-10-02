using WebApi.Contracts;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly BookingManagementDbContext _context;

        public EmployeeRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public Employee? Create(Employee employee)
        {
            try
            {
                //ORM melakukan Create
                _context.Set<Employee>().Add(employee);
                _context.SaveChanges();
                return employee;

            }
            catch
            {
                return null;
            }
        }

        public bool Delete(Employee employee)
        {
            try
            {
                //ORM melakukan Remove
                _context.Set<Employee>().Remove(employee);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Set<Employee>().ToList(); // ORM melakukan get all
        }

        public Employee? GetByGuid(Guid guid)
        {
            return _context.Set<Employee>().Find(guid); // ORM melakukan get by guid
        }

        public bool Update(Employee employee)
        {
            try
            {
                //ORM melakukan Update
                _context.Set<Employee>().Update(employee);
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
