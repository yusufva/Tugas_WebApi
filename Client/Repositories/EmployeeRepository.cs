using Client.Contracts;
using WebApi.DTOs.Employees;
using WebApi.Models;

namespace Client.Repositories
{
    public class EmployeeRepository : GeneralRepository<EmployeesDto, NewEmployeesDto, Guid>, IEmployeeRepository
    {
        public EmployeeRepository(string request = "Employee/") : base(request)
        {
        }
    }
}
