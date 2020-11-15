using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentingSystem.Controllers
{
    [Authorize]
    public class InfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}