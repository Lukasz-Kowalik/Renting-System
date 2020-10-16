using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.Helpers.Attributes;
using RentingSystemAPI.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentingSystemAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Rent>>> GetRents()
        //{
        //    return await _context.Rents.ToListAsync();
        //}

        [HttpGet("{userId}")]
        //  [Authorize]
        public async Task<ActionResult<List<Rent>>> GetRents(int userId)
        {
            var query = new GetAllRentsByUserIdQuery(userId);
            var result = await _mediator.Send(query);
            return !result.Any() ? (ActionResult<List<Rent>>)NoContent() : Ok(result);
        }
    }
}