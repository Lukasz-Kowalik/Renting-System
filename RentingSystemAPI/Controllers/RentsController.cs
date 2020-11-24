using Microsoft.AspNetCore.Mvc;
using RentingSystemAPI.Interfaces;
using System;
using System.Threading.Tasks;

namespace RentingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentsController : ControllerBase
    {
        private readonly IRentService _rentService;

        public RentsController(IRentService rentService)
        {
            _rentService = rentService;
        }

        [HttpPost]
        [Route("AddRents")]
        public async Task<IActionResult> AddItems([FromQuery] string email = null)
        {
            try
            {
                var result = await _rentService.Add(User, email);
                return result ? (IActionResult)Ok() : BadRequest();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}