using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs.Account;
using WebApi.DTOs.Employees;

namespace Client.Controllers
{
    [Authorize(Roles = "Manager, Admin")] //menambahkan role akses
    public class AccountController : Controller
    {
        private readonly IAccountRepository repository;
        public AccountController(IAccountRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var result = await repository.Get();
            var listAccount = new List<AccountsDto>();
            if (result != null)
            {
                listAccount = result.Data.ToList();
            }

            return View(listAccount);
        }
    }
}
