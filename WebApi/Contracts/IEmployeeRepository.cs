using WebApi.DTOs.Employees;
using WebApi.Models;

namespace WebApi.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        Employee? GetByEmail(string email);
        EmployeeRegisterRequestDto? Register(EmployeeRegisterRequestDto request, bool isValid);
    }
}
