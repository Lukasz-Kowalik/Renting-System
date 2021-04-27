using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentingSystem.Models;

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

        [Authorize(Policy = nameof(AccountTypes.Admin))]
        public IActionResult Index()
        {
            return View();
        }
    }
}