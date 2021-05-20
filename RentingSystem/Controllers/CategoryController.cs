using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentingSystem.Models;

namespace RentingSystem.Controllers
{
    [Authorize(Policy = nameof(AccountTypes.Admin))]
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("Category/Edit/{id}")]
        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
    }
}