using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RentingSystem.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}