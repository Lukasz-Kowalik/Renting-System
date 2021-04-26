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

        /// <summary>
        /// Get rented items for user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRentedItems")]
        public ActionResult<IEnumerable<RentedItemsResponse>> GetRents(string email)
        {
            var result = _rentedItemsService.Get(User, email);
            return Ok(result);
        }

        /// <summary>
        /// Get rented items for all users
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllRentedItems")]
        public ActionResult<IEnumerable<RentedItemsResponseWithUsers>> GetAllRents()
        {
            var result = _rentedItemsService.GetAll();
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