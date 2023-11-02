using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.AccountRoles;
using WebApi.DTOs.Rooms;

namespace Client.Controllers
{
    [Authorize]
    public class RoomController : Controller
    {
        private readonly IRoomRepository repository;
        public RoomController(IRoomRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var result = await repository.Get();
            var list = new List<RoomDto>();
            if (result != null)
            {
                list = result.Data.ToList();
            }

            return View(list);
        }
    }
}
