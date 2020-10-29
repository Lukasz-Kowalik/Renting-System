using AutoMapper;
using RentingSystemAPI.BAL.Entities;
using RentingSystemAPI.DAL.Context;
using RentingSystemAPI.DTOs.Request;
using RentingSystemAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentingSystemAPI.Services
{
    public class CartService : ICartService
    {
        private readonly IMapper _mapper;
        private readonly RentingContext _context;
        private readonly IUserService _userService;

        public CartService(
            IMapper mapper,
            RentingContext context, IUserService userService)
        {
            _mapper = mapper;
            _context = context;
            _userService = userService;
        }

        public IEnumerable<Item> GetUserCart(int userId)
        {
            return _context.Carts
                .Where(x => x.UserId == userId)
                .GroupBy(g => new
                {
                    g.UserId,
                    g.ItemId,
                    g.Name
                })
                .Select(i =>
                    new Item
                    {
                        ItemId = i.Key.ItemId,
                        Name = i.Key.Name,
                        Quantity = i.ToList().Sum(x => x.Quantity)
                    })
                .OrderBy(i => i.ItemId);
        }

        public Cart GetUserCartItem(int userId, int itemId)
        {
            return _context.Carts
                .FirstOrDefault(x => x.UserId == userId && x.ItemId == itemId);
        }

        public async Task AddItemToCart(AddItemRequest request, ClaimsPrincipal user)
        {
            var item = _context.Items.FirstOrDefault(x => x.ItemId == request.ItemId);
            if (item is null) throw new DataException("item not found");
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (item.Quantity > 0)
                {
                    var itemToCart = new Cart
                    {
                        UserId = _userService.GetUserId(user, request.Email),
                        ItemId = item.ItemId,
                        Quantity = 1,
                        Name = item.Name
                    };

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

        public async Task RemoveFromCart(RemoveItemFromCartRequest request, ClaimsPrincipal user)
        {
            var userId = _userService.GetUserId(user, request.Email);
            var cart = GetUserCartItem(userId, request.ItemId);

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (cart != null)
                {
                    var item = _context.Items.FirstOrDefault(x => x.ItemId == request.ItemId);
                    if (item == null) throw new Exception("Lack of item");
                    item.Quantity++;
                    _context.Items.Update(item);
                    await _context.SaveChangesAsync();

                    _context.Carts.Remove(cart);
                    await _context.SaveChangesAsync();
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}