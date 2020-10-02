using Microsoft.AspNetCore.Mvc;

namespace RentingSystem.bin.Controllers
{
    public class AdminPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}