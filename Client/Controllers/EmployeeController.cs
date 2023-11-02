using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApi.DTOs.Employees;
using WebApi.Models;

namespace Client.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository repository;
        public EmployeeController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        [Authorize(Roles = "Manager, Admin")] //menambahkan role akses
        public async Task<IActionResult> Index()
        {
            var result = await repository.Get();
            var listEmployee = new List<EmployeesDto>();
            if (result != null)
            {
                listEmployee = result.Data.ToList();
            }

            return View(listEmployee);
        }

        [Authorize]
        [HttpGet("Logout/")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index");
        }
    }
}