using Microsoft.AspNetCore.Mvc;
using RentingSystemAPI.DTOs.Request;
using RentingSystemAPI.DTOs.Response;
using RentingSystemAPI.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentedItemsController : ControllerBase
    {
        private readonly IRentedItemsService _rentedItemsService;

        public RentedItemsController(IRentedItemsService rentedItemsService)
        {
            _rentedItemsService = rentedItemsService;
        }

        [HttpGet]
        [Route("GetRentedItems")]
        public ActionResult<IEnumerable<RentedItemsResponse>> GetRents(string email)
        {
            var result = _rentedItemsService.Get(User, email);
            return Ok(result);
        }

        [HttpPatch]
        [Route("ReturnItems")]
        public async Task<IActionResult> Return(ReturnItemsRequest request)
        {
            var result = await _rentedItemsService.ReturnItems(User, request);

            return result ? (IActionResult)Ok() : NotFound();
        }
    }
}