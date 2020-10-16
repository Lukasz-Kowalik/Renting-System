using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentingSystem.Controllers
{
    public class UserPanelController : Controller
    {
        public IActionResult Item()
        {
            return View();
        }

        //  [Authorize(Policy = "Admin")]
        public IActionResult Rented()
        {
            return View();
        }
    }
}