using WebApi.Contracts;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly BookingManagementDbContext _context;

        public AccountsRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public Accounts? Create(Accounts accounts)
        {
            try
            {
                //ORM melakukan Create
                _context.Set<Accounts>().Add(accounts);
                _context.SaveChanges();
                return accounts;

            }
            catch
            {
                return null;
            }
        }

        public bool Delete(Accounts accounts)
        {
            try
            {
                //ORM melakukan Remove
                _context.Set<Accounts>().Remove(accounts);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Accounts> GetAll()
        {
            return _context.Set<Accounts>().ToList(); // ORM melakukan get all
        }

        public Accounts? GetByGuid(Guid guid)
        {
            var entity = _context.Set<Accounts>().Find(guid);
            _context.ChangeTracker.Clear();
            return entity; // ORM melakukan get by guid
        }

        public bool Update(Accounts accounts)
        {
            try
            {
                //ORM melakukan Update
                _context.Set<Accounts>().Update(accounts);
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
