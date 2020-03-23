using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RentingSystem.Controllers
{
    public class UserPanelController : Controller
    {
        public IActionResult Item()
        {
            return View();
        }
        public IActionResult Rented()
        {
            return View();
        }
    }
}