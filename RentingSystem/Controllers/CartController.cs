using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentingSystem.Models;
using System.Threading.Tasks;

namespace RentingSystem.Controllers
{
    [Authorize(Policy = nameof(AccountTypes.User))]
    public class CartController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}