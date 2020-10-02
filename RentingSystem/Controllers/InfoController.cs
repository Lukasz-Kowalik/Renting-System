using Microsoft.AspNetCore.Mvc;

namespace RentingSystem.bin.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}