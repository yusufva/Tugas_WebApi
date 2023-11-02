using Client.Contracts;
using WebApi.DTOs.Employees;
using WebApi.DTOs.Roles;
using WebApi.Models;

namespace Client.Repositories
{
    public class RoleRepository : GeneralRepository<RoleDto, NewRoleDto, Guid>, IRoleRepository
    {
        public RoleRepository(string request = "Role/") : base(request)
        {
        }
    }
}
