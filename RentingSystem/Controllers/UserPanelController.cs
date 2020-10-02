using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentingSystem.bin.Controllers
{
    public class UserPanelController : Controller
    {
        public IActionResult Item()
        {
            return View();
        }

        [Authorize]
        public IActionResult Rented()
        {
            return View();
        }
    }
}