using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RentingSystem.Controllers
{
    public class CartController : Controller
    {
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}