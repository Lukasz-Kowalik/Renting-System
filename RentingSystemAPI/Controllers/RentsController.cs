using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.Interfaces;
using System;
using System.Threading.Tasks;

namespace RentingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly RentingContext _context;
        private readonly IRentService _rentService;

        public RentsController(IMediator mediator, RentingContext context, IRentService rentService)
        {
            _mediator = mediator;
            _context = context;
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
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}