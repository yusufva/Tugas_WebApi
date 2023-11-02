using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.AccountRoles;
using WebApi.DTOs.Universities;

namespace Client.Controllers
{
    [Authorize]
    public class UniversityController : Controller
    {
        private readonly IUniversityRepository repository;
        public UniversityController(IUniversityRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            return View(/*list*/);
        }

        public async Task<JsonResult> GetAll()
        {
            var result = await repository.Get();
            return Json(result.Data);
        }
    }
}
