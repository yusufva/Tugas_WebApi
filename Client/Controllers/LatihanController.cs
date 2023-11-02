using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers
{
    [Authorize]
    public class LatihanController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Pokemon()
        {
            return View();
        }


        [Authorize(Roles = "Manager, Admin")] //menambahkan role akses
        public IActionResult CRUD()
        {
            return View();
        }
    }
}