using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.Account;
using WebApi.DTOs.AccountRoles;

namespace Client.Controllers
{
    [Authorize(Roles = "Manager, Admin")] //menambahkan role akses
    public class AccountRoleController : Controller
    {
        private readonly IAccountRolesRepository repository;
        public AccountRoleController(IAccountRolesRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var result = await repository.Get();
            var list = new List<AccountRolesDto>();
            if (result != null)
            {
                list = result.Data.ToList();
            }

            return View(list);
        }
    }
}
