using Microsoft.AspNetCore.Mvc;

namespace RentingSystem.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}