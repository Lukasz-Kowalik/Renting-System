using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentingSystem.bin.Controllers
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