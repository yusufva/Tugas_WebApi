using WebApi.DTOs.Employees;
using WebApi.DTOs.Roles;
using WebApi.Models;

namespace Client.Contracts
{
    public interface IRoleRepository : IRepository<RoleDto, NewRoleDto, Guid>
    {
    }
}
