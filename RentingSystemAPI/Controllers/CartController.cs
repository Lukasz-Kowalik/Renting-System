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
using Microsoft.EntityFrameworkCore;
using RentingSystemAPI.DTOs.Response;

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

        [HttpGet]
        [Route("GetCart")]
        public async Task<IActionResult> Get()
        {
            var user = _userManager.GetUserAsync(User);
            var userCart = _context.Carts.Where(c => c.UserId == user.Id).GroupBy(g => g.Items);
            var response = _mapper.Map<ItemListResponse>(userCart);
            return Ok(response);
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

                    itemToCart.UserId = Int32.Parse(_userManager.GetUserId(User));
                    // if (user == null) throw new Exception("user doesn't exist");
                    //itemToCart.UserId = user.Id;

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