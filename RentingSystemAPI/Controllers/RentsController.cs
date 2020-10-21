using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.Helpers.Attributes;
using RentingSystemAPI.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentingSystemAPI.DAL.Context;

namespace RentingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly RentingContext _context;

        public RentsController(IMediator mediator, RentingContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rent>>> GetRents()
        {
            return await _context.Rents.ToListAsync();
        }

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