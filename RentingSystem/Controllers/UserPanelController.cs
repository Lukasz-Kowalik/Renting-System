using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentingSystem.Controllers
{
    [Authorize]
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

        public IActionResult Index()
        {
            return View();
        }
    }
}