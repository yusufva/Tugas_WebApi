using WebApi.Models;

namespace WebApi.Contracts
{
    public interface IAccountsRepository
    {
        IEnumerable<Accounts> GetAll();
        Accounts? GetByGuid(Guid guid);
        Accounts? Create(Accounts accounts);
        bool Update(Accounts accounts);
        bool Delete(Accounts accounts);
    }
}
