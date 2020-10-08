using Microsoft.AspNetCore.Mvc;

namespace RentingSystem.Controllers
{
    public class AdminPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}