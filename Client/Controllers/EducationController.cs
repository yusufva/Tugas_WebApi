using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.AccountRoles;
using WebApi.DTOs.Educations;

namespace Client.Controllers
{
    [Authorize]
    public class EducationController : Controller
    {
        private readonly IEducationRepository repository;
        public EducationController(IEducationRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var result = await repository.Get();
            var list = new List<EducationsDto>();
            if (result != null)
            {
                list = result.Data.ToList();
            }

            return View(list);
        }
    }
}
