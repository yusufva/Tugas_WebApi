﻿using Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers
{
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

        public IActionResult CRUD()
        {
            return View();
        }
    }
}