using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.DTOs.Request;
using RentingSystemAPI.DTOs.Response;
using RentingSystemAPI.Interfaces;
using RentingSystemAPI.Validators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RentingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;

        public CartController(IMapper mapper,
             IUserService userService, ICartService cartService)
        {
            _mapper = mapper;
            _userService = userService;
            _cartService = cartService;
        }

        [HttpGet]
        [Route("GetCart")]
        public ActionResult<IEnumerable<ItemListResponse>> Get(string email = null)
        {
            try
            {
                var userId = _userService.GetUserId(User, email);
                var userCart = _cartService.GetUserCart(userId);
                var response = _mapper.Map<ItemListResponse[]>(userCart);
                return Ok(response);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] AddItemRequest request)
        {
            try
            {
                var validator = new CartValidator<AddItemRequest>();
                var result = await validator.ValidateAsync(request);
                if (!result.IsValid)
                {
                    throw new ValidationException("Invalid data");
                }

                await _cartService.AddItemToCart(request, User);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("Remove")]
        public async Task<IActionResult> Remove([FromBody] RemoveItemFromCartRequest request)
        {
            try
            {
                var validator = new CartValidator<RemoveItemFromCartRequest>();
                var result = await validator.ValidateAsync(request);
                if (!result.IsValid)
                {
                    throw new ValidationException("Invalid data");
                }

                await _cartService.RemoveFromCart(request, User);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}