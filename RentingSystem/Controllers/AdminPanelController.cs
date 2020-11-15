using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentingSystem.Models;

namespace RentingSystem.Controllers
{
    [Authorize(Policy = nameof(AccountTypes.Admin))]
    public class AdminPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}