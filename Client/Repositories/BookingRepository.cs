using Client.Contracts;
using WebApi.DTOs.Bookings;
using WebApi.DTOs.Employees;
using WebApi.Models;

namespace Client.Repositories
{
    public class BookingRepository : GeneralRepository<BookingsDto, NewBookingsDto, Guid>, IBookingRepository
    {
        public BookingRepository(string request = "Booking/") : base(request)
        {
        }
    }
}
