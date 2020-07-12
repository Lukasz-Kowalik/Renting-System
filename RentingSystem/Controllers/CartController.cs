using Microsoft.AspNetCore.Mvc;

namespace RentingSystem.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}