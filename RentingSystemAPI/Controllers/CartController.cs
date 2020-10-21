using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.DTOs.Request;
using RentingSystemAPI.Validators;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Add([FromBody] AddItemRequest request)
        {
            try
            {
                var validator = new CartValidator();
                var result = await validator.ValidateAsync(request);
                if (!result.IsValid)
                {
                    throw new ValidationException("Invalid data");
                }

                await AddItemToCart(request);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        private async Task AddItemToCart(AddItemRequest request)
        {
            var item = _context.Items.FirstOrDefault(x => x.ItemId == request.ItemId);
            if (item is null) throw new DataException("item not found");
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (item.Quantity > 0)
                {
                    var itemToCart = _mapper.Map<Cart>(request);
                    var user = await _userManager.GetUserAsync(User);
                    await _context.Carts.AddAsync(itemToCart);
                    await _context.SaveChangesAsync();
                    item.Quantity--;
                    _context.Items.Update(item);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}