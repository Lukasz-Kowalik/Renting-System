using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentingSystem.Models;

namespace RentingSystem.Controllers
{
    public class CartController : Controller
    {
        //  [Authorize(Policy = "User")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}