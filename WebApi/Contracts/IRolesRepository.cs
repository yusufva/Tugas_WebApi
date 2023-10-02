using WebApi.Models;

namespace WebApi.Contracts
{
    public interface IRolesRepository
    {
        IEnumerable<Role> GetAll();
        Role? GetByGuid(Guid guid);
        Role? Create(Role role);
        bool Update(Role role);
        bool Delete(Role role);
    }
}
