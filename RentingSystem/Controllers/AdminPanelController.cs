using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Renting_System.Controllers
{
    public class AdminPanelController : Controller
    {
        public IActionResult Borrowers()
        {
            return View();
        }
    }
}