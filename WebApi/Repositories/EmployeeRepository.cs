using WebApi.Contracts;
using WebApi.Data;
using WebApi.Models;
using WebApi.Utilities.Handler;

namespace WebApi.Repositories
{
    public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BookingManagementDbContext context, GenerateHandler generateHandler) : base(context)
        {
        }

        public Employee? GetByEmail(string email)
        {
            var employee = _context.Set<Employee>().FirstOrDefault(e => e.Email == email); //mengambil data employee berdasarkan email

            return employee;
        }

        
    }
}
