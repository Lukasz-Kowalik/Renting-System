using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentingSystem.Controllers
{
    public class CartController : Controller
    {
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}