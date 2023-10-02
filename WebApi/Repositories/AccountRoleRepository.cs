﻿using WebApi.Contracts;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class AccountRoleRepository : IAccountRoleRepository
    {
        private readonly BookingManagementDbContext _context;

        public AccountRoleRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public AccountRole? Create(AccountRole accountRole)
        {
            try
            {
                //ORM melakukan Create
                _context.Set<AccountRole>().Add(accountRole);
                _context.SaveChanges();
                return accountRole;

            }
            catch
            {
                return null;
            }
        }

        public bool Delete(AccountRole accountRole)
        {
            try
            {
                //ORM melakukan Remove
                _context.Set<AccountRole>().Remove(accountRole);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<AccountRole> GetAll()
        {
            return _context.Set<AccountRole>().ToList(); // ORM melakukan get all
        }

        public AccountRole? GetByGuid(Guid guid)
        {
            return _context.Set<AccountRole>().Find(guid); // ORM melakukan get by guid
        }

        public bool Update(AccountRole accountRole)
        {
            try
            {
                //ORM melakukan Update
                _context.Set<AccountRole>().Update(accountRole);
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
