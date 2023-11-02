using WebApi.DTOs.Employees;
using WebApi.Models;

namespace Client.Contracts
{
    public interface IEmployeeRepository : IRepository<EmployeesDto, NewEmployeesDto, Guid>
    {
    }
}
