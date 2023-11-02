using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.AccountRoles;
using WebApi.DTOs.Bookings;

namespace Client.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingRepository repository;
        public BookingController(IBookingRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var result = await repository.Get();
            var list = new List<BookingsDto>();
            if (result != null)
            {
                list = result.Data.ToList();
            }

            return View(list);
        }
    }
}
