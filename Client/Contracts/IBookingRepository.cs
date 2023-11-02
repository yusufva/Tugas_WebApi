using WebApi.DTOs.Bookings;
using WebApi.DTOs.Employees;
using WebApi.Models;

namespace Client.Contracts
{
    public interface IBookingRepository : IRepository<BookingsDto, NewBookingsDto, Guid>
    {
    }
}
