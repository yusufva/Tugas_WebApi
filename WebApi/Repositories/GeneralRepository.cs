using WebApi.Data;
using WebApi.Utilities.Handler;

namespace WebApi.Repositories
{
    public class GeneralRepository<Tentity> where Tentity : class
    {
        protected readonly BookingManagementDbContext _context;

        public GeneralRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public Tentity? Create(Tentity entity)
        {
            try
            {
                //ORM melakukan Create
                _context.Set<Tentity>().Add(entity);
                _context.SaveChanges();
                return entity;

            }
            catch (Exception ex) 
            {
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message); //melemparkan error
            }
        }

        public bool Delete(Tentity entity)
        {
            try
            {
                //ORM melakukan Remove
                _context.Set<Tentity>().Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException is not null && ex.InnerException.Message.Contains("IX_tb_m_employees_nik"))
                {
                    throw new ExceptionHandler("NIK already exists");
                }
                if (ex.InnerException is not null && ex.InnerException.Message.Contains("IX_tb_m_employees_email"))
                {
                    throw new ExceptionHandler("Email already exists");
                }
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_tb_m_employees_phone_number"))
                {
                    throw new ExceptionHandler("Phone number already exists");
                }
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message); //melemparkan error
            }
        }

        public IEnumerable<Tentity> GetAll()
        {
            return _context.Set<Tentity>().ToList(); // ORM melakukan get all
        }

        public Tentity? GetByGuid(Guid guid)
        {
            var entity = _context.Set<Tentity>().Find(guid);
            _context.ChangeTracker.Clear();
            return entity; // ORM melakukan get by guid
        }

        public bool Update(Tentity entity)
        {
            try
            {
                //ORM melakukan Update
                _context.Set<Tentity>().Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message); //melemparkan error
            }
        }
    }
}
