using MediatR;
using RentingSystemAPI.Helpers.Attributes;
using Microsoft.AspNetCore.Mvc;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentingSystemAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class RentedItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentedItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}")]
        [Authorize]
        public async Task<ActionResult<List<RentedItem>>> GetRentedItems(int? userId)
        {
            var query = new GetRentedItemsByUserQuery(userId);
            var result = await _mediator.Send(query);
            return !result.Any() ? (ActionResult<List<RentedItem>>)NoContent() : Ok(result);
        }
    }
}