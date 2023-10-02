using WebApi.Contracts;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingManagementDbContext _context;

        public BookingRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public Booking? Create(Booking booking)
        {
            try
            {
                //ORM melakukan Create
                _context.Set<Booking>().Add(booking);
                _context.SaveChanges();
                return booking;

            }
            catch
            {
                return null;
            }
        }

        public bool Delete(Booking booking)
        {
            try
            {
                //ORM melakukan Remove
                _context.Set<Booking>().Remove(booking);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Booking> GetAll()
        {
            return _context.Set<Booking>().ToList(); // ORM melakukan get all
        }

        public Booking? GetByGuid(Guid guid)
        {
            return _context.Set<Booking>().Find(guid); // ORM melakukan get by guid
        }

        public bool Update(Booking booking)
        {
            try
            {
                //ORM melakukan Update
                _context.Set<Booking>().Update(booking);
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
