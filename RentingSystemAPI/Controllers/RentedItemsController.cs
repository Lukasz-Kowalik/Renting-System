using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using RentingSystemAPI.Queries;

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
            var tocken = await HttpContext.GetTokenAsync("token");
            var query = new GetRentedItemsByUserQuery(userId);
            var result = await _mediator.Send(query);
            return !result.Any() ? (ActionResult<List<RentedItem>>)NoContent() : Ok(result);
        }
    }
}