using WebApi.Contracts;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly BookingManagementDbContext _context;

        public RoomRepository (BookingManagementDbContext context)
        {
            _context = context;
        }

        public Room? Create(Room room)
        {
            try
            {
                //ORM melakukan Create
                _context.Set<Room>().Add(room);
                _context.SaveChanges();
                return room;

            }
            catch
            {
                return null;
            }
        }

        public bool Delete(Room room)
        {
            try
            {
                //ORM melakukan Remove
                _context.Set<Room>().Remove(room);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Room> GetAll()
        {
            return _context.Set<Room>().ToList(); // ORM melakukan get all
        }

        public Room? GetByGuid(Guid guid)
        {
            return _context.Set<Room>().Find(guid); // ORM melakukan get by guid
        }

        public bool Update(Room room)
        {
            try
            {
                //ORM melakukan Update
                _context.Set<Room>().Update(room);
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
