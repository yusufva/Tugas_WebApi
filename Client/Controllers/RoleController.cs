using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.AccountRoles;
using WebApi.DTOs.Roles;

namespace Client.Controllers
{
    [Authorize(Roles = "Admin")] //menambahkan role akses
    public class RoleController : Controller
    {
        private readonly IRoleRepository repository;
        public RoleController(IRoleRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var result = await repository.Get();
            var list = new List<RoleDto>();
            if (result != null)
            {
                list = result.Data.ToList();
            }

            return View(list);
        }
    }
}
