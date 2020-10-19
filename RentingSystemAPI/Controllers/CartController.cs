using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.DTOs.Request;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using RentingSystemAPI.Validators;

namespace RentingSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly RentingContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public CartController(RentingContext context, IMapper mapper,
            UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] AddItemRequest item)
        {
            try
            {
                var validator = new CartValidator();
                var result = validator.Validate(item);
                if (!result.IsValid)
                {
                    throw new ValidationException("Invalid data");
                }
                var itemToCart = _mapper.Map<Cart>(item);
                var user = await _userManager.GetUserAsync(User);
                itemToCart.User = user;
                var exist = _context.Carts.Find(itemToCart);
                if (exist != null)
                {
                    await _context.Carts.AddAsync(itemToCart);
                }
                else
                {
                    exist.Quantity++;
                    _context.Carts.Update(exist);
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}